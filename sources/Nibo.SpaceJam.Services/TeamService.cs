using Nibo.SpaceJam.Infraestructure;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using Nibo.SpaceJam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Services
{
    /// <summary>
    /// Implementation of team service features
    /// </summary>
    public class TeamService : ServiceBase<TeamModel>, ITeamService
    {
        private readonly IRepository<OperationLogModel> _repositoryOfOperationLog;

        /// <inheritdoc />
        public TeamService(IRepository<TeamModel> repository, IRepository<OperationLogModel> repositoryOfOperationLog) : base(repository)
        {
            _repositoryOfOperationLog = repositoryOfOperationLog;
            base.OnPersist += TeamService_OnPersist; ;
        }

        /// <inheritdoc />
        private async void TeamService_OnPersist(IList<OperationLogModel> auditList)
        {
            foreach (var item in auditList)
                await this._repositoryOfOperationLog.CreateOrUpdateAsync(item);
        }

        /// <inheritdoc />
        public override async Task<TeamModel> CreateOrUpdateAsync(TeamModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var validationResult = model.Validate();

            var teamCount = await base._repository.CountAsync(x => !x.Id.Equals(model.Id) && x.Name.Equals(model.Name));

            if (teamCount > 0)
                validationResult.Errors.Add("Name", "TeamService_CreateOrUpdateAsync_Validation_UniqueTeamName".Translate());

            if (!validationResult.IsValid)
                throw new Infraestructure.ValidationException("ValidationException".Translate(), validationResult.Errors);

            return await base.CreateOrUpdateAsync(model);
        }
    }
}