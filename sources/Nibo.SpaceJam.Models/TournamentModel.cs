using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data structure for tournaments
    /// </summary>
    public class TournamentModel : BaseModel<TournamentModel>
    {
        /// <summary>
        /// Name of tournament
        /// </summary>
        [Display(Name = "TournamentModel_Name_Display_Name", Description = "TournamentModel_Name_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string Name { get; set; }

        /// <summary>
        /// Stage of tournament
        /// </summary>
        [Display(Name = "TournamentModel_Stage_Display_Name", Description = "TournamentModel_Stage_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string Stage { get; set; }
    }
}