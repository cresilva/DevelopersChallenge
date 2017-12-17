using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nibo.SpaceJam.Infraestructure.ValueObjects
{
    /// <summary>
    /// Structure for model validation result
    /// </summary>
    public class ModelValidationResult
    {
        /// <summary>
        /// List of errors
        /// </summary>
        public Dictionary<string, string> Errors { get; private set; }

        /// <summary>
        /// Model is valid?
        /// </summary>
        public bool IsValid => !Errors.Any();

        /// <summary>
        /// Initialize validation result
        /// </summary>
        /// <param name="errors">List of errors</param>
        public ModelValidationResult(Dictionary<string, string> errors = null)
        {
            Errors = errors ?? new Dictionary<string, string>();
        }
    }
}