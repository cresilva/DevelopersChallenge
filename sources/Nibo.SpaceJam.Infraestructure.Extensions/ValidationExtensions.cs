using Nibo.SpaceJam.Infraestructure.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Feature for business and models validations
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Validate model
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="model">Instance of model</param>
        /// <returns>Validation results</returns>
        public static ModelValidationResult Validate<TModel>(this TModel model)
        {
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, new ValidationContext(model, null, null), validationResults, true);

            var validationDictionary = new Dictionary<string, string>();

            foreach (var item in validationResults)
                validationDictionary.Add(item.MemberNames.First(), item.ErrorMessage);

            var result = new ModelValidationResult(validationDictionary);

            if (!result.IsValid)
                throw new Nibo.SpaceJam.Infraestructure.ValidationException("ValidationException".Translate(), result.Errors);

            return result;
        }
    }
}