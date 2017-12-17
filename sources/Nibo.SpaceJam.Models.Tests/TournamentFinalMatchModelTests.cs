using System;
using Xunit;

namespace Nibo.SpaceJam.Models.Tests
{
    public class TournamentFinalMatchModelTests
    {
        [Fact]
        public void Ctor_ValidateTeamAIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new TournamentFinalMatchModel(null, new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team B" }); });
        }

        [Fact]
        public void Ctor_ValidateTeamBIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new TournamentFinalMatchModel(new TeamModel() { Id = Guid.NewGuid().ToString(), Name = "Team A" }, null); });
        }
    }
}