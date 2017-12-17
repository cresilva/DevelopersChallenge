using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data structure for final match
    /// </summary>
    public class TournamentFinalMatchModel
    {
        /// <summary>
        /// Initialize final match
        /// </summary>
        /// <param name="teamA">Team A informations</param>
        /// <param name="teamB">Team B informations</param>
        public TournamentFinalMatchModel(TeamModel teamA, TeamModel teamB)
        {
            this.TeamA = teamA ?? throw new ArgumentNullException("TournamentFinalMatchModel_TeamA_Required".Translate(), nameof(teamA));
            this.TeamB = teamB ?? throw new ArgumentNullException("TournamentFinalMatchModel_TeamB_Required".Translate(), nameof(teamB));
        }

        /// <summary>
        /// Team A information
        /// </summary>
        [Display(Name = "TournamentFinalMatchModel_TeamA_Display_Name", Description = "TournamentFinalMatchModel_TeamA_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel TeamA { get; private set; }

        /// <summary>
        /// Team B information
        /// </summary>
        [Display(Name = "TournamentFinalMatchModel_TeamB_Display_Name", Description = "TournamentFinalMatchModel_TeamB_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel TeamB { get; private set; }
    }
}