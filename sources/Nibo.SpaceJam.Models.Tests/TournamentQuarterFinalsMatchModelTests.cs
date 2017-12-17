using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nibo.SpaceJam.Models.Tests
{
    public class TournamentQuarterFinalsMatchModelTests
    {
        private readonly TeamModel[] _keyWithOneTeam;
        private readonly TeamModel[] _keyOne;
        private readonly TeamModel[] _keyTwo;
        private readonly TeamModel[] _keyTree;
        private readonly TeamModel[] _keyFour;

        #region Ctor

        public TournamentQuarterFinalsMatchModelTests()
        {
            _keyWithOneTeam = new TeamModel[] { new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Uniq Team" } };

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

            _keyTree = new TeamModel[2]
            {
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team E" },
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team F" },
            };

            _keyFour = new TeamModel[2]
            {
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team G" },
                new TeamModel(){ Id = Guid.NewGuid().ToString(), Name = "Team H" },
            };
        }

        #endregion

        #region Key one tests

        [Fact]
        public void Ctor_ValidatekeyOneIsNull_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(null, _keyTwo, _keyTree, _keyFour); });
        }

        [Fact]
        public void Ctor_ValidatekeyOneHasMinRequiredTeams_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyWithOneTeam, _keyTwo, _keyTree, _keyFour); });
        }

        #endregion

        #region Key two tests

        [Fact]
        public void Ctor_ValidatekeyTwoIsNull_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyOne, null, _keyTree, _keyFour); });
        }

        [Fact]
        public void Ctor_ValidatekeyTwoHasMinRequiredTeams_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyOne, _keyWithOneTeam, _keyTree, _keyFour); });
        }

        #endregion

        #region Key tree tests

        [Fact]
        public void Ctor_ValidatekeyTreeIsNull_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyOne, _keyTwo, null, _keyFour); });
        }

        [Fact]
        public void Ctor_ValidatekeyTreeHasMinRequiredTeams_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyOne, _keyTwo, _keyWithOneTeam, _keyFour); });
        }

        #endregion

        #region Key four tests

        [Fact]
        public void Ctor_ValidatekeyFourIsNull_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyOne, _keyTwo, _keyTree, null); });
        }

        [Fact]
        public void Ctor_ValidatekeyFourHasMinRequiredTeams_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new TournamentQuarterFinalsMatchModel(_keyOne, _keyTwo, _keyTree, _keyWithOneTeam); });
        }

        #endregion
    }
}