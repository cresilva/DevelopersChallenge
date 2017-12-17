using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data structure for quarter finals match
    /// </summary>
    public class TournamentQuarterFinalsMatchModel
    {
        /// <summary>
        /// Initialize keys of semi finals
        /// </summary>
        /// <param name="keyOne">Teams of key one</param>
        /// <param name="keyTwo">Teams of key two</param>
        /// <param name="keyTree">Teams of key tree</param>
        /// <param name="keyFour">Teams of key four</param>
        public TournamentQuarterFinalsMatchModel(TeamModel[] keyOne, TeamModel[] keyTwo, TeamModel[] keyTree, TeamModel[] keyFour)
        {
            //Validate if keys has two teams
            if (keyOne == null || !keyOne.Any() || !keyOne.Count().Equals(2)) throw new ArgumentException("Validation_TournamentMatchKeys_Minimum".Translate(), nameof(keyOne));
            if (keyTwo == null || !keyTwo.Any() || !keyTwo.Count().Equals(2)) throw new ArgumentException("Validation_TournamentMatchKeys_Minimum".Translate(), nameof(keyTwo));
            if (keyTree == null || !keyTree.Any() || !keyTree.Count().Equals(2)) throw new ArgumentException("Validation_TournamentMatchKeys_Minimum".Translate(), nameof(keyTree));
            if (keyFour == null || !keyFour.Any() || !keyFour.Count().Equals(2)) throw new ArgumentException("Validation_TournamentMatchKeys_Minimum".Translate(), nameof(keyFour));

            this.KeyOne = keyOne;
            this.KeyTwo = keyTwo;
            this.KeyTree = keyTree;
            this.KeyFour = keyFour;
        }

        /// <summary>
        /// Teams in key one
        /// </summary>
        [Display(Name = "TournamentQuarterFinalsMatchModel_KeyOne_Display_Name", Description = "TournamentQuarterFinalsMatchModel_KeyOne_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel[] KeyOne { get; private set; }

        /// <summary>
        /// Teams in key two
        /// </summary>
        [Display(Name = "TournamentQuarterFinalsMatchModel_KeyTwo_Display_Name", Description = "TournamentQuarterFinalsMatchModel_KeyTwo_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel[] KeyTwo { get; private set; }

        /// <summary>
        /// Teams in key tree
        /// </summary>
        [Display(Name = "TournamentQuarterFinalsMatchModel_KeyTree_Display_Name", Description = "TournamentQuarterFinalsMatchModel_KeyTree_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel[] KeyTree { get; private set; }

        /// <summary>
        /// Teams in key four
        /// </summary>
        [Display(Name = "TournamentQuarterFinalsMatchModel_KeyFour_Display_Name", Description = "TournamentQuarterFinalsMatchModel_KeyFour_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public TeamModel[] KeyFour { get; private set; }
    }
}