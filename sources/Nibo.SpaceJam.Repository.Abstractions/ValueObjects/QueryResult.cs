using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.SpaceJam.Repository.Abstractions.ValueObjects
{
    /// <summary>
    /// Structure for query result
    /// </summary>
    /// <typeparam name="TValueObject"></typeparam>
    public class QueryResult<TValueObject>
    {
        /// <summary>
        /// Index of page
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Registers per page
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Total of records
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// Total of pages
        /// </summary>
        public long PageCount { get; set; }

        /// <summary>
        /// List of paged registers
        /// </summary>
        public IEnumerable<TValueObject> ResultList { get; set; }
    }
}
