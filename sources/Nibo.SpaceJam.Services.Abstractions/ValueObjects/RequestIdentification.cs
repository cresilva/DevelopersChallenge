using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.SpaceJam.Services.Abstractions.ValueObjects
{
    /// <summary>
    /// Structure for request identification
    /// </summary>
    public class RequestIdentification
    {
        /// <summary>
        /// Origin of request
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Resposable user login
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public string UserAddress { get; set; }
    }
}
