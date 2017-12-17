using Nibo.SpaceJam.Repository.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Repository.Abstractions
{
    /// <summary>
    /// Common features for CRUD operations
    /// </summary>
    /// <typeparam name="TModel">Type of model</typeparam>
    public interface IRepository<TModel>
    {
        /// <summary>
        /// Get total count
        /// </summary>
        /// <returns></returns>
        Task<long> CountAsync();

        /// <summary>
        /// Get count using expression filter
        /// </summary>
        /// <param name="expression">Expression to filter</param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<TModel, bool>> expression);

        /// <summary>
        /// Get model using id
        /// </summary>
        /// <param name="id">Id of model</param>
        /// <returns></returns>
        Task<TModel> GetByIdAsync(string id);

        /// <summary>
        /// Get single model using expression filter
        /// </summary>
        /// <param name="expression">Expression to filter</param>
        /// <returns></returns>
        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> expression);

        /// <summary>
        /// List all models
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TModel>> ListAllAsync();

        /// <summary>
        /// Query models
        /// </summary>
        /// <param name="criteria">Query criteria</param>
        /// <returns></returns>
        Task<QueryResult<TModel>> PagedQueryAsync(QueryCriteria<TModel> criteria);

        /// <summary>
        /// Create if id is not defined, update for existent models
        /// </summary>
        /// <param name="model">Model to save</param>
        /// <returns></returns>
        Task<TModel> CreateOrUpdateAsync(TModel model);

        /// <summary>
        /// Delete a list of models
        /// </summary>
        /// <param name="model">List for models to delete</param>
        /// <returns></returns>
        Task DeleteAsync(IList<TModel> model);

        /// <summary>
        /// Delete a single model
        /// </summary>
        /// <param name="model">Model to delete</param>
        /// <returns></returns>
        Task DeleteAsync(TModel model);

        /// <summary>
        /// Delete model using id
        /// </summary>
        /// <param name="id">Id of model to delete</param>
        /// <returns></returns>
        Task DeleteAsync(string id);
    }
}
