using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nibo.SpaceJam.Models.Tests
{
    public class TournamentResultModelTests
    {
        [Fact]
        public void Ctor_ValidateTournamentIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new TournamentResultModel(null); });
        }

        [Fact]
        public void Winner_CheckWinnerIsValid_ArgumentException()
        {
            var teamA = new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team A" };
            var teamB = new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team B" };
            var teamWinner = new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team C" };

            Assert.Throws<ArgumentException>(() =>
            {
                new TournamentResultModel(Guid.NewGuid().ToString())
                {
                    Final = new TournamentFinalMatchModel(teamA, teamB),
                    Winner = teamWinner
                };
            });
        }
    }
}