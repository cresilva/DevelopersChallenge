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
    public class TournamentResultServiceTests
    {
        private readonly Mock<IRepository<TournamentResultModel>> _mockOfRepository;
        private readonly TournamentResultService _service;
        private readonly TeamModel[] _keyOne;
        private readonly TeamModel[] _keyTwo;
        private readonly TeamModel[] _keyTree;
        private readonly TeamModel[] _keyFour;
        private readonly TournamentQuarterFinalsMatchModel _tournamentQuarterFinalsMatchModel;
        private readonly TournamentSemiFinalsMatchModel _tournamentSemiFinalsMatchModel;
        private readonly TournamentFinalMatchModel _tournamentFinalMatchModel;

        #region Ctor

        public TournamentResultServiceTests()
        {
            this._mockOfRepository = new Mock<IRepository<TournamentResultModel>>();

            this._service = new TournamentResultService(this._mockOfRepository.Object,
                new Mock<IRepository<OperationLogModel>>().Object,
                new Mock<IRepository<TeamModel>>().Object,
                new Mock<IRepository<TournamentModel>>().Object)
            {
                RequestIdentification = new Abstractions.ValueObjects.RequestIdentification()
                {
                    Origin = "Tests",
                    UserAddress = "127.0.0.1",
                    UserLogin = "cristianoeugenio"
                }
            };

            _keyOne = new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team A" }, new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team B" } };
            _keyTwo = new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team C" }, new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team D" } };
            _keyTree = new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team E" }, new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team F" } };
            _keyFour = new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team G" }, new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team H" } };

            _tournamentQuarterFinalsMatchModel = new TournamentQuarterFinalsMatchModel(_keyOne, _keyTwo, _keyTree, _keyFour);
            _tournamentSemiFinalsMatchModel = new TournamentSemiFinalsMatchModel(_keyOne, _keyTwo);
            _tournamentFinalMatchModel = new TournamentFinalMatchModel(_keyOne[0], _keyTwo[0]);
        }

        #endregion

        #region RegisterQuarterFinals tests

        [Fact]
        public async void RegisterQuarterFinals_CheckTournamentIdIsNull_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await this._service.RegisterQuarterFinals(null, _tournamentQuarterFinalsMatchModel);
            });
        }

        [Fact]
        public async void RegisterQuarterFinals_CheckTournamentExists_NotFoundException()
        {
            Expression<Func<IRepository<TournamentResultModel>, Task<TournamentResultModel>>> getSingleAsync = p => p.GetSingleAsync(It.IsAny<Expression<Func<TournamentResultModel, bool>>>());

            this._mockOfRepository.Setup(getSingleAsync).Returns(Task.FromResult(default(TournamentResultModel))).Verifiable();

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await this._service.RegisterQuarterFinals("123", _tournamentQuarterFinalsMatchModel);
            });

            this._mockOfRepository.Verify(getSingleAsync, Times.Once);
        }

        [Fact]
        public async void RegisterQuarterFinals_CheckInvalidData_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await this._service.RegisterQuarterFinals("123", new TournamentQuarterFinalsMatchModel(new TeamModel[] { }, this._keyTwo, this._keyTree, this._keyFour));
            });
        }

        #endregion
    }
}
