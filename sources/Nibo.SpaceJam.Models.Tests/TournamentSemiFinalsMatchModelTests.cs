using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nibo.SpaceJam.Models.Tests
{
    public class TournamentSemiFinalsMatchModelTests
    {
        private readonly TeamModel[] _keyOne;
        private readonly TeamModel[] _keyTwo;

        #region Ctor

        public TournamentSemiFinalsMatchModelTests()
        {
            _keyOne = new TeamModel[2]
            {
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team A" },
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team B" },
            };

            _keyTwo = new TeamModel[2]
            {
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team C" },
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team D" },
            };
        }

        #endregion

        #region Key one tests

        [Fact]
        public void Ctor_ValidatekeyOneIsNull_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentSemiFinalsMatchModel(null, _keyTwo); });
        }

        [Fact]
        public void Ctor_ValidatekeyOneHasMinRequiredTeams_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentSemiFinalsMatchModel(new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team A" } }, _keyTwo); });
        }

        #endregion

        #region Key two tests

        [Fact]
        public void Ctor_ValidatekeyTwoIsNull_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentSemiFinalsMatchModel(_keyOne, null); });
        }

        [Fact]
        public void Ctor_ValidatekeyTwoHasMinRequiredTeams_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentSemiFinalsMatchModel(_keyOne, new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team C" } }); });
        }

        #endregion
    }
}