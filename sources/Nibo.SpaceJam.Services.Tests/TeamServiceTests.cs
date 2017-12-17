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
    public class TeamServiceTests
    {
        private readonly Mock<IRepository<TeamModel>> _mockOfRepository;
        private readonly TeamService _service;

        public TeamServiceTests()
        {
            this._mockOfRepository = new Mock<IRepository<TeamModel>>();
            this._service = new TeamService(this._mockOfRepository.Object, new Mock<IRepository<OperationLogModel>>().Object)
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
                await this._service.CreateOrUpdateAsync(new TeamModel());
            });
        }

        [Fact]
        public async void CreateOrUpdateAsync_CheckNameIsUnique_ValidationException()
        {
            Expression<Func<IRepository<TeamModel>, Task<long>>> countAsync = p => p.CountAsync(It.IsAny<Expression<Func<TeamModel, bool>>>());

            this._mockOfRepository.Setup(countAsync).Returns(Task.FromResult(long.MaxValue)).Verifiable();

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await this._service.CreateOrUpdateAsync(new TeamModel() { Name = "Team A" });
            });

            this._mockOfRepository.Verify(countAsync, Times.Once);
        }
    }
}