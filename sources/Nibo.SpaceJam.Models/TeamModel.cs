using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data structure for teams
    /// </summary>
    public class TeamModel: BaseModel<TeamModel>
    {
        /// <summary>
        /// Name of team
        /// </summary>
        [Display(Name = "TeamModel_Name_Display_Name", Description = "TeamModel_Name_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string Name { get; set; }
    }
}
