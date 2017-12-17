using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nibo.SpaceJam.Models
{
    /// <summary>
    /// Data sctructure for operation logs
    /// </summary>
    public class OperationLogModel : BaseModel<OperationLogModel>
    {
        /// <summary>
        /// Origin of operation
        /// </summary>
        [Display(Name = "OperationLogModel_Origin_Display_Name", Description = "OperationLogModel_Origin_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string Origin { get; set; }

        /// <summary>
        /// Type of operation
        /// </summary>
        [Display(Name = "OperationLogModel_OperationType_Display_Name", Description = "OperationLogModel_OperationType_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string OperationType { get; set; }

        /// <summary>
        /// Type of model
        /// </summary>
        [Display(Name = "OperationLogModel_ModelType_Display_Name", Description = "OperationLogModel_ModelType_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string ModelType { get; set; }

        /// <summary>
        /// Id of register
        /// </summary>
        [Display(Name = "OperationLogModel_ModelId_Display_Name", Description = "OperationLogModel_ModelId_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string ModelId { get; set; }

        /// <summary>
        /// Old state
        /// </summary>
        [Display(Name = "OperationLogModel_OldState_Display_Name", Description = "OperationLogModel_OldState_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string OldState { get; set; }

        /// <summary>
        /// New state
        /// </summary>
        [Display(Name = "OperationLogModel_NewState_Display_Name", Description = "OperationLogModel_NewState_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string NewState { get; set; }

        /// <summary>
        /// User responsable for operation
        /// </summary>
        [Display(Name = "OperationLogModel_UserLogin_Display_Name", Description = "OperationLogModel_UserLogin_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string UserLogin { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        [Display(Name = "OperationLogModel_UserAddress_Display_Name", Description = "OperationLogModel_UserAddress_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public string UserAddress { get; set; }

        /// <summary>
        /// Date of operation
        /// </summary>
        [Display(Name = "OperationLogModel_OperationDate_Display_Name", Description = "OperationLogModel_OperationDate_Display_Description", ResourceType = typeof(Infraestructure.Resources.Models))]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Infraestructure.Resources.Models))]
        public DateTime OperationDate { get; set; }
    }
}
