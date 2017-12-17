using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data structure for semi finals match
    /// </summary>
    public class TournamentSemiFinalsMatchModel
    {
        /// <summary>
        /// Initialize keys of semi finals
        /// </summary>
        /// <param name="keyOne">Teams of key one</param>
        /// <param name="keyTwo">Teams of key two</param>
        public TournamentSemiFinalsMatchModel(TeamModel[] keyOne, TeamModel[] keyTwo)
        {
            //Validate if keys has two teams
            if (keyOne == null || !keyOne.Any() || !keyOne.Count().Equals(2)) throw new ArgumentException("Validation_TournamentMatchKeys_Minimum".Translate(), nameof(keyOne));
            if (keyTwo == null || !keyTwo.Any() || !keyTwo.Count().Equals(2)) throw new ArgumentException("Validation_TournamentMatchKeys_Minimum".Translate(), nameof(keyTwo));

            this.KeyOne = keyOne;
            this.KeyTwo = keyTwo;
        }

        /// <summary>
        /// Teams in key one
        /// </summary>
        [Display(Name = "TournamentSemiFinalsMatchModel_KeyOne_Display_Name", Description = "TournamentSemiFinalsMatchModel_KeyOne_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel[] KeyOne { get; private set; }

        /// <summary>
        /// Teams in key two
        /// </summary>
        [Display(Name = "TournamentSemiFinalsMatchModel_KeyTwo_Display_Name", Description = "TournamentSemiFinalsMatchModel_KeyTwo_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel[] KeyTwo { get; private set; }
    }
}