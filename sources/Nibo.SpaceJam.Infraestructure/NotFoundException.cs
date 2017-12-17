using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.SpaceJam.Infraestructure
{
    /// <summary>
    /// Custom exceptions for not found object result
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initialize exception
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
