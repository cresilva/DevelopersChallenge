using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nibo.SpaceJam.Infraestructure
{
    /// <summary>
    /// Custom exceptions for vaidations
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// List of validation erros
        /// </summary>
        public Dictionary<string, string> Errors { get; private set; }

        /// <summary>
        /// Initialize exception
        /// </summary>
        /// <param name="message">Exception message</param>
        public ValidationException(string message) : base(message) { }

        /// <summary>
        /// Initialize exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="errors">List of errors</param>
        public ValidationException(string message, Dictionary<string, string> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
