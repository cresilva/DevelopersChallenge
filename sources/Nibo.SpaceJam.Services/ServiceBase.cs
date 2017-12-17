using Newtonsoft.Json;
using Nibo.SpaceJam.Infraestructure;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using Nibo.SpaceJam.Repository.Abstractions.ValueObjects;
using Nibo.SpaceJam.Services.Abstractions;
using Nibo.SpaceJam.Services.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Services
{
    /// <summary>
    /// Implementation features for CRUD operations
    /// </summary>
    /// <typeparam name="TModel">Type of model</typeparam>
    public class ServiceBase<TModel> : IService<TModel>
    {
        internal readonly IRepository<TModel> _repository;
        private readonly IList<OperationLogModel> _operationLog;

        #region Public events

        /// <summary>
        /// Event triggered after save or delete operations
        /// </summary>
        public event OnPersistHandler OnPersist;

        /// <summary>
        /// Event executed results
        /// </summary>
        /// <param name="auditList">Audit values</param>
        public delegate void OnPersistHandler(IList<OperationLogModel> auditList);

        #endregion

        /// <inheritdoc />
        public RequestIdentification RequestIdentification { get; set; }

        /// <inheritdoc />
        public ServiceBase(IRepository<TModel> repository)
        {
            _repository = repository;
            _operationLog = new List<OperationLogModel>();
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync()
        {
            return await this._repository.CountAsync();
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync(Expression<Func<TModel, bool>> expression = null)
        {
            return await this._repository.CountAsync(expression);
        }

        /// <inheritdoc />
        public virtual async Task<TModel> GetByIdAsync(string id, string notFoundExceptionMessage = null)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("Validation_Required_Id".Translate());

            var result = await this._repository.GetByIdAsync(id);

            if (!string.IsNullOrEmpty(notFoundExceptionMessage) && result == null) throw new NotFoundException(notFoundExceptionMessage);

            return result;
        }

        /// <inheritdoc />
        public virtual async Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> expression, string notFoundExceptionMessage = null)
        {
            var result = await this._repository.GetSingleAsync(expression);

            if (!string.IsNullOrEmpty(notFoundExceptionMessage) && result == null) throw new NotFoundException(notFoundExceptionMessage);

            return result;
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TModel>> ListAllAsync()
        {
            return await this._repository.ListAllAsync();
        }

        /// <inheritdoc />
        public virtual async Task<QueryResult<TModel>> PagedQueryAsync(QueryCriteria<TModel> criteria)
        {
            return await this._repository.PagedQueryAsync(criteria);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(string id, string notFoundExceptionMessage = null)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("Validation_Required_Id".Translate());

            var currentInstance = await this.GetByIdAsync(id);

            if (currentInstance != null)
            {
                Audity(currentInstance, default(TModel), "Delete");
                await this._repository.DeleteAsync(id);
            }
            else if (!string.IsNullOrEmpty(notFoundExceptionMessage) && currentInstance == null) throw new NotFoundException(notFoundExceptionMessage);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(TModel model, string notFoundExceptionMessage = null)
        {
            var currentInstance = await this.GetByIdAsync((model as BaseModel<TModel>).Id);

            if (currentInstance != null)
            {
                Audity(currentInstance, default(TModel), "Delete");
                await this._repository.DeleteAsync(currentInstance);
            }
            else if (!string.IsNullOrEmpty(notFoundExceptionMessage) && currentInstance == null) throw new NotFoundException(notFoundExceptionMessage);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(IList<TModel> model)
        {
            foreach (var item in model)
            {
                var currentInstance = await this.GetByIdAsync((item as BaseModel<TModel>).Id);

                if (currentInstance != null)
                {
                    Audity(currentInstance, default(TModel), "Delete");
                    await this._repository.DeleteAsync(currentInstance);
                }
            }
        }

        /// <inheritdoc />
        public virtual async Task<TModel> CreateOrUpdateAsync(TModel model)
        {
            var validationResult = model.Validate();

            if (!validationResult.IsValid)
                throw new ValidationException("Exception_ValidationModel".Translate(), validationResult.Errors);

            var entity = model as BaseModel<TModel>;
            var currentInstance = default(TModel);

            if (!string.IsNullOrEmpty(entity.Id))
            {
                currentInstance = await this.GetByIdAsync(entity.Id);

                if (currentInstance != null)
                    Audity(currentInstance, model, "Update");
                else
                    throw new NotFoundException("NotFoundException".Translate());
            }
            else
                Audity(default(TModel), model, "Create");

            return await this._repository.CreateOrUpdateAsync(model);
        }

        /// <inheritdoc />
        private void Audity(TModel currentInstance, TModel newInstance, string operationLogType)
        {
            if (this.RequestIdentification == null) throw new ArgumentNullException(nameof(RequestIdentification));

            this._operationLog.Add(new OperationLogModel()
            {
                OperationType = operationLogType,
                OperationDate = DateTime.Now,
                ModelId = currentInstance != null ? (currentInstance as BaseModel<TModel>).Id : null,
                ModelType = typeof(TModel).Name.Replace("Model", ""),
                OldState = currentInstance != null ? JsonConvert.SerializeObject(currentInstance) : null,
                NewState = newInstance != null ? JsonConvert.SerializeObject(newInstance) : null,
                Origin = this.RequestIdentification.Origin,
                UserAddress = this.RequestIdentification.UserAddress,
                UserLogin = this.RequestIdentification.UserLogin
            });

            this.OnPersist?.Invoke(this._operationLog);
        }
    }
}