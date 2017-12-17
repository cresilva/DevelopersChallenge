using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data structure for tournament results
    /// </summary>
    public class TournamentResultModel : BaseModel<TournamentResultModel>
    {
        private TeamModel _winner;

        /// <summary>
        /// Parameterless constructor required for linq expressions
        /// </summary>
        public TournamentResultModel() { }

        /// <summary>
        /// Initialize tournament result
        /// </summary>
        /// <param name="tournamentId">Tournament id</param>
        public TournamentResultModel(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrWhiteSpace(tournamentId))
                throw new ArgumentNullException("TournamentResultModel_Tournament_Required".Translate(), nameof(tournamentId));

            this.Tournament = tournamentId;
        }

        /// <summary>
        /// Tournament informations
        /// </summary>
        [Display(Name = "TournamentResultModel_Tournament_Display_Name", Description = "TournamentResultModel_Tournament_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string Tournament { get; set; }

        /// <summary>
        /// Quarter finals match
        /// </summary>
        [Display(Name = "TournamentResultModel_QuarterFinals_Display_Name", Description = "TournamentResultModel_QuarterFinals_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        public TournamentQuarterFinalsMatchModel QuarterFinals { get; set; }

        /// <summary>
        /// Semi finals match
        /// </summary>
        [Display(Name = "TournamentResultModel_SemiFinals_Display_Name", Description = "TournamentResultModel_SemiFinals_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        public TournamentSemiFinalsMatchModel SemiFinals { get; set; }

        /// <summary>
        /// Final match
        /// </summary>
        [Display(Name = "TournamentResultModel_Final_Display_Name", Description = "TournamentResultModel_Final_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        public TournamentFinalMatchModel Final { get; set; }

        /// <summary>
        /// Tournament winner
        /// </summary>
        [Display(Name = "TournamentResultModel_Winner_Display_Name", Description = "TournamentResultModel_Winner_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel Winner
        {
            get { return _winner; }
            set
            {
                if (Final != null)
                {
                    if (value != null && !value.Id.Equals(Final.TeamA.Id) && !value.Id.Equals(Final.TeamB.Id)) throw new ArgumentException("TournamentResultModel_Winner_Invalid".Translate(), nameof(Winner));

                    _winner = value;
                }
            }
        }
    }
}