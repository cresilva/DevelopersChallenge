using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nibo.SpaceJam.Repository.Abstractions.ValueObjects
{
    /// <summary>
    /// Structure for query criteria
    /// </summary>
    /// <typeparam name="TValueObject">Type of filter</typeparam>
    public class QueryCriteria<TValueObject>
    {
        /// <summary>
        /// Initialize criteria
        /// </summary>
        /// <param name="filter">Filter expression</param>
        public QueryCriteria(Expression<Func<TValueObject, bool>> filter)
        {
            this.Offset = 0;
            this.Limit = 20;
            this.Filter = filter;
            this.OrderBy = new List<string>();
            this.OrderByDescending = new List<string>();
        }

        /// <summary>
        /// Number of skips
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Limit of registers
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Filter expression
        /// </summary>
        public Expression<Func<TValueObject, bool>> Filter { get; private set; }

        /// <summary>
        /// Ascending columns
        /// </summary>
        public IList<string> OrderBy { get; set; }

        /// <summary>
        /// Descending columns
        /// </summary>
        public IList<string> OrderByDescending { get; set; }
    }
}
