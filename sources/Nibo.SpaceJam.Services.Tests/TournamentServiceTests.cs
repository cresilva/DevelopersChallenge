using Moq;
using Nibo.SpaceJam.Infraestructure;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nibo.SpaceJam.Services.Tests
{
    public class TournamentServiceTests
    {
        private readonly Mock<IRepository<TournamentModel>> _mockOfRepository;
        private readonly TournamentService _service;

        public TournamentServiceTests()
        {
            this._mockOfRepository = new Mock<IRepository<TournamentModel>>();
            this._service = new TournamentService(this._mockOfRepository.Object, new Mock<IRepository<OperationLogModel>>().Object, new Mock<IRepository<TournamentResultModel>>().Object)
            {
                RequestIdentification = new Abstractions.ValueObjects.RequestIdentification()
                {
                    Origin = "Tests",
                    UserAddress = "127.0.0.1",
                    UserLogin = "cristianoeugenio"
                }
            };
        }

        [Fact]
        public async void CreateOrUpdateAsync_CheckModelIsNull_ArgumentNullExceptioin()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await this._service.CreateOrUpdateAsync(null);
            });
        }

        [Fact]
        public async void CreateOrUpdateAsync_CheckNameIsNull_ValidationException()
        {
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await this._service.CreateOrUpdateAsync(new TournamentModel() { Stage = "Stage One" });
            });
        }
        
        [Fact]
        public async void CreateOrUpdateAsync_CheckStageIsNull_ValidationException()
        {
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await this._service.CreateOrUpdateAsync(new TournamentModel() { Name = "Tournament One" });
            });
        }

        [Fact]
        public async void CreateOrUpdateAsync_CheckNameIsUnique_ValidationException()
        {
            Expression<Func<IRepository<TournamentModel>, Task<long>>> countAsync = p => p.CountAsync(It.IsAny<Expression<Func<TournamentModel, bool>>>());

            this._mockOfRepository.Setup(countAsync).Returns(Task.FromResult(long.MaxValue)).Verifiable();

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await this._service.CreateOrUpdateAsync(new TournamentModel() { Name = "Tournament One", Stage = "Stage one" });
            });

            this._mockOfRepository.Verify(countAsync, Times.Once);
        }
    }
}
