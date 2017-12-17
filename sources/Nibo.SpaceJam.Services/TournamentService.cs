using Nibo.SpaceJam.Infraestructure;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using Nibo.SpaceJam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Services
{
    /// <summary>
    /// Implementation of tournament service features
    /// </summary>
    public class TournamentService : ServiceBase<TournamentModel>, ITournamentService
    {
        private readonly IRepository<OperationLogModel> _repositoryOfOperationLog;
        private readonly IRepository<TournamentResultModel> _tournamentResultRepository;

        /// <inheritdoc />
        public TournamentService(IRepository<TournamentModel> repository,
            IRepository<OperationLogModel> repositoryOfOperationLog,
            IRepository<TournamentResultModel> tournamentResultRepository) : base(repository)
        {
            _repositoryOfOperationLog = repositoryOfOperationLog;
            _tournamentResultRepository = tournamentResultRepository;

            base.OnPersist += TournamentService_OnPersist;
        }

        /// <inheritdoc />
        private async void TournamentService_OnPersist(IList<OperationLogModel> auditList)
        {
            foreach (var item in auditList)
                await this._repositoryOfOperationLog.CreateOrUpdateAsync(item);
        }

        /// <inheritdoc />
        public override async Task<TournamentModel> CreateOrUpdateAsync(TournamentModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var isNewTournament = string.IsNullOrEmpty(model.Id);

            var validationResult = model.Validate();
            var tournamentCount = await base._repository.CountAsync(x => !x.Id.Equals(model.Id) && x.Name.Equals(model.Name));

            if (tournamentCount > 0)
                validationResult.Errors.Add("Name", "TournamentService_CreateOrUpdateAsync_Validation_UniqueTournamentName".Translate());

            if (!validationResult.IsValid)
                throw new Infraestructure.ValidationException("ValidationException".Translate(), validationResult.Errors);

            if (isNewTournament)
                model.Stage = "WAITING";

            model = await base.CreateOrUpdateAsync(model);

            if (isNewTournament)
                await _tournamentResultRepository.CreateOrUpdateAsync(new TournamentResultModel(model.Id));

            return model;
        }

        /// <inheritdoc />
        public override async Task DeleteAsync(string id, string notFoundExceptionMessage = null)
        {
            var results = await this._tournamentResultRepository.GetSingleAsync(x => x.Tournament.Equals(id));

            //Delete tournaments results
            await _tournamentResultRepository.DeleteAsync(results);

           await base.DeleteAsync(id, notFoundExceptionMessage);
        }
    }
}