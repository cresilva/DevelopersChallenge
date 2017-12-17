using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using Nibo.SpaceJam.Repository.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Repository
{
    /// <summary>
    /// Implementation features for Azure CosmosDB
    /// </summary>
    /// <typeparam name="TModel">Type of model</typeparam>
    public class CosmosRepository<TModel> : IRepository<TModel>, IDisposable where TModel : BaseModel<TModel>
    {
        private readonly ICosmosWrapper _cosmosWrapper;
        //private readonly DocumentCollection _collection;
        private readonly FeedOptions _feedOptions;
        private readonly IOrderedQueryable<TModel> _query;
        private readonly Uri _collectionUri;
        private readonly string _collectionId;

        /// <inheritdoc />
        public CosmosRepository(ICosmosWrapper cosmosWrapper)
        {
            _cosmosWrapper = cosmosWrapper;

            _feedOptions = new FeedOptions()
            {
                EnableCrossPartitionQuery = true,
                EnableScanInQuery = true,
                EnableLowPrecisionOrderBy = true
            };

            _collectionId = typeof(TModel).Name.Replace("Model", "");
            _collectionUri = UriFactory.CreateDocumentCollectionUri(this._cosmosWrapper.DatabaseName, this._collectionId);

            var collection = new DocumentCollection() { Id = _collectionId };
            collection.IndexingPolicy.IncludedPaths.Add(new IncludedPath { Path = "/*", Indexes = new Collection<Index> { new RangeIndex(DataType.String) { Precision = -1 } } });

            cosmosWrapper.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(cosmosWrapper.DatabaseName), collection);

            _query = this._cosmosWrapper.CreateDocumentQuery<TModel>(_collectionUri, _feedOptions);
        }

        /// <inheritdoc />
        public async virtual Task<long> CountAsync()
        {
            return await Task.FromResult(this._query.Count());
        }

        /// <inheritdoc />
        public async virtual Task<long> CountAsync(Expression<Func<TModel, bool>> expression)
        {
            return await Task.FromResult(this._query.Where(expression).Count());
        }

        /// <inheritdoc />
        public async virtual Task<TModel> CreateOrUpdateAsync(TModel model)
        {
            var entity = model as BaseModel<TModel>;

            var result = await this._cosmosWrapper.UpsertDocumentAsync(this._collectionUri, model, null, false);

            model.Id = result.Resource.Id;

            return model;
        }

        /// <inheritdoc />
        public async virtual Task DeleteAsync(IList<TModel> model)
        {
            foreach (var item in model)
                await this.DeleteAsync(item);
        }

        /// <inheritdoc />
        public async virtual Task DeleteAsync(TModel model)
        {
            await this._cosmosWrapper.DeleteDocumentAsync((UriFactory.CreateDocumentUri(this._cosmosWrapper.DatabaseName, this._collectionId, model.Id)));
        }

        /// <inheritdoc />
        public async virtual Task DeleteAsync(string id)
        {
            await this._cosmosWrapper.DeleteDocumentAsync((UriFactory.CreateDocumentUri(this._cosmosWrapper.DatabaseName, this._collectionId, id)));
        }

        /// <inheritdoc />
        public async virtual Task<TModel> GetByIdAsync(string id)
        {
            TModel result = default(TModel);

            try
            {
                var response = await this._cosmosWrapper.ReadDocumentAsync<TModel>(UriFactory.CreateDocumentUri(this._cosmosWrapper.DatabaseName, this._collectionId, id));

                result = response.Document;
            }
            catch (DocumentClientException exception)
            {
                if (exception.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
            }

            return result;
        }

        /// <inheritdoc />
        public async virtual Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> expression)
        {
            return await Task.FromResult(this._query.Where(expression).AsEnumerable().SingleOrDefault());
        }

        /// <inheritdoc />
        public async virtual Task<IEnumerable<TModel>> ListAllAsync()
        {
            return await Task.FromResult(this._query.AsEnumerable());
        }

        /// <inheritdoc />
        public async virtual Task<QueryResult<TModel>> PagedQueryAsync(QueryCriteria<TModel> criteria)
        {
            var result = new QueryResult<TModel>()
            {
                Total = await this.CountAsync(),
                Offset = criteria.Offset,
                Limit = criteria.Limit
            };

            return await Task.Factory.StartNew(() =>
            {
                var query = (criteria.Filter != null) ? this._query.Where(criteria.Filter) : this._query;

                if (criteria.OrderBy.Any())
                    query = criteria.OrderBy.Aggregate(query, (current, column) => current.OrderBy(column, "asc"));

                if (criteria.OrderByDescending.Any())
                    query = criteria.OrderByDescending.Aggregate(query, (current, column) => current.OrderBy(column, "desc"));

                result.ResultList = query.AsEnumerable().Skip(criteria.Offset).Take(criteria.Limit).ToList<TModel>();
                result.PageCount = (long)Math.Ceiling((double)result.Total / result.Limit);

                return result;
            });
        }

        #region IDisposable

        /// <inheritdoc />
        public virtual void Dispose()
        {

        }

        #endregion
    }

}
