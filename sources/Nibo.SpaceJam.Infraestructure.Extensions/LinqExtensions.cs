using System;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// Implements extensions for query building using linq
    /// </summary>
    public static partial class LinqExtensions
    {
        /// <summary>
        /// Create custom ordered query
        /// </summary>
        /// <param name="query">Source query</param>
        /// <param name="sortColumn">Column to order</param>
        /// <param name="sortOrder">Order direction</param>
        /// <returns>Ordered query of T</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string sortOrder)
        {
            var methodName = $"OrderBy{(sortOrder.ToLower() == "asc" || sortOrder.ToLower() == "ascending" ? "" : "descending")}";

            var parameter = Expression.Parameter(query.ElementType, "p");

            var memberAccess = sortColumn.Split('.').Aggregate<string, MemberExpression>(null, (current, property) => Expression.Property(current ?? (parameter as Expression), property));

            var result = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { query.ElementType, memberAccess.Type },
                query.Expression,
                Expression.Quote(Expression.Lambda(memberAccess, parameter)));

            return query.Provider.CreateQuery<T>(result);
        }

        #region Random

        /// <summary>
        /// Return random item from query
        /// </summary>
        /// <param name="query">Source query</param>
        /// <returns>Random Instance of T</returns>
        public static T RandomElement<T>(this IQueryable<T> query)
        {
            return query.Skip(new Random((int)DateTime.Now.Ticks).Next(query.Count())).First();
        }

        /// <summary>
        /// Return randomized items from query
        /// </summary>
        /// <param name="query">Source query</param>
        /// <param name="limit">Number of items to return</param>
        /// <returns>Random list of T</returns>
        public static IQueryable<T> RandomElements<T>(this IQueryable<T> query, int limit)
        {
            return query.OrderBy(r => new Random((int)DateTime.Now.Ticks).Next()).Take(limit).AsQueryable();
        }

        #endregion
    }

}
