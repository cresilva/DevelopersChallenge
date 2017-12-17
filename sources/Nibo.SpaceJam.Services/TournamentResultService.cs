using Nibo.SpaceJam.Infraestructure;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Repository.Abstractions;
using Nibo.SpaceJam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Services
{
    /// <summary>
    /// Implementation of tournament results service features
    /// </summary>
    public class TournamentResultService : ServiceBase<TournamentResultModel>, ITournamentResultService
    {
        private readonly IRepository<TournamentModel> _tournamentRepository;
        private readonly IRepository<OperationLogModel> _repositoryOfOperationLog;
        private readonly IRepository<TeamModel> _teamRepository;

        /// <inheritdoc />
        public TournamentResultService(IRepository<TournamentResultModel> repository,
            IRepository<OperationLogModel> repositoryOfOperationLog, IRepository<TeamModel> teamRepository, IRepository<TournamentModel> tournamentRepository) : base(repository)
        {
            _tournamentRepository = tournamentRepository;
            _repositoryOfOperationLog = repositoryOfOperationLog;
            _teamRepository = teamRepository;
            base.OnPersist += TournamentResultService_OnPersist;
        }

        /// <inheritdoc />
        private async void TournamentResultService_OnPersist(IList<OperationLogModel> auditList)
        {
            foreach (var item in auditList)
                await this._repositoryOfOperationLog.CreateOrUpdateAsync(item);
        }

        /// <inheritdoc />
        public async Task RegisterQuarterFinals(string tournamentId, TournamentQuarterFinalsMatchModel tournamentQuarterFinals)
        {
            if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrWhiteSpace(tournamentId)) throw new ArgumentNullException("TournamentResultService_Validation_Required_TournamentId", nameof(tournamentId));

            var result = await this._repository.GetSingleAsync(x => x.Tournament.Equals(tournamentId));

            if (result == null)
                throw new NotFoundException("TournamentResultService_Validation_TournamentNotFound".Translate());

            result.QuarterFinals = tournamentQuarterFinals;

            await base.CreateOrUpdateAsync(result);
            await this.UpdateTournamentStage(result.Tournament, "QUARTER_FINALS");
        }

        /// <inheritdoc />
        public async Task RegisterSemiFinals(string tournamentId, TournamentSemiFinalsMatchModel tournamentSemiFinals)
        {
            if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrWhiteSpace(tournamentId)) throw new ArgumentNullException("TournamentResultService_Validation_Required_TournamentId", nameof(tournamentId));

            var result = await this._repository.GetSingleAsync(x => x.Tournament.Equals(tournamentId));

            if (result == null)
                throw new NotFoundException("TournamentResultService_Validation_TournamentNotFound".Translate());

            if (result.QuarterFinals == null)
                throw new ValidationException("TournamentResultService_RegisterSemiFinals_Validation_QuarterFinalsNotFound");

            result.SemiFinals = tournamentSemiFinals;

            if (!result.QuarterFinals.KeyOne.Any(x => tournamentSemiFinals.KeyOne.Select(i => i.Id).Contains(x.Id)))
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_InvalidSemiFinalTeam");

            if (!result.QuarterFinals.KeyTwo.Any(x => tournamentSemiFinals.KeyOne.Select(i => i.Id).Contains(x.Id)))
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_InvalidSemiFinalTeam");

            if (!result.QuarterFinals.KeyTree.Any(x => tournamentSemiFinals.KeyTwo.Select(i => i.Id).Contains(x.Id)))
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_InvalidSemiFinalTeam");

            if (!result.QuarterFinals.KeyFour.Any(x => tournamentSemiFinals.KeyTwo.Select(i => i.Id).Contains(x.Id)))
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_InvalidSemiFinalTeam");

            await base.CreateOrUpdateAsync(result);
            await this.UpdateTournamentStage(result.Tournament, "SEMI_FINALS");
        }

        /// <inheritdoc />
        public async Task RegisterFinal(string tournamentId, TournamentFinalMatchModel tournamentFinal)
        {
            if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrWhiteSpace(tournamentId)) throw new ArgumentNullException("TournamentResultService_Validation_Required_TournamentId", nameof(tournamentId));

            var result = await this._repository.GetSingleAsync(x => x.Tournament.Equals(tournamentId));

            if (result == null)
                throw new NotFoundException("TournamentResultService_Validation_TournamentNotFound".Translate());

            if (result.SemiFinals == null)
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_SemiFinalsNotFound");

            result.Final = tournamentFinal;

            if (!result.SemiFinals.KeyOne.Any(x => x.Id.Equals(tournamentFinal.TeamA.Id)))
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_InvalidFinalTeam");

            if (!result.SemiFinals.KeyTwo.Any(x => x.Id.Equals(tournamentFinal.TeamB.Id)))
                throw new ValidationException("TournamentResultService_RegisterFinal_Validation_InvalidFinalTeam");

            await base.CreateOrUpdateAsync(result);
            await this.UpdateTournamentStage(result.Tournament, "FINAL");
        }

        /// <inheritdoc />
        public async Task Registerwinner(string tournamentId, string teamId)
        {
            if (string.IsNullOrEmpty(tournamentId) || string.IsNullOrWhiteSpace(tournamentId)) throw new ArgumentNullException("TournamentResultService_Validation_Required_TournamentId", nameof(tournamentId));
            if (string.IsNullOrEmpty(teamId) || string.IsNullOrWhiteSpace(teamId)) throw new ArgumentNullException("TournamentResultService_Validation_Required_TeamId", nameof(teamId));

            var result = await this._repository.GetSingleAsync(x => x.Tournament.Equals(tournamentId));

            if (result == null)
                throw new NotFoundException("TournamentResultService_Validation_TournamentNotFound".Translate());

            if (result.Final == null)
                throw new ValidationException("TournamentResultService_RegisterWinner_Validation_FinalNotFound");

            result.Winner = await _teamRepository.GetByIdAsync(teamId);

            if (result.Winner == null)
                throw new NotFoundException("TournamentResultService_RegisterWinner_Validation_TeamNotFound".Translate());

            //Check if winner team is valid
            if (!teamId.Equals(result.Final.TeamA.Id) && !teamId.Equals(result.Final.TeamB.Id))
                throw new ValidationException("TournamentResultService_RegisterWinner_Validation_WinnerInvalid");

            await base.CreateOrUpdateAsync(result);
            await this.UpdateTournamentStage(result.Tournament, "CLOSED");
        }

        private async Task UpdateTournamentStage(string tournamentId, string stage)
        {
            var tournament = await this._tournamentRepository.GetByIdAsync(tournamentId);

            tournament.Stage = stage;

            await this._tournamentRepository.CreateOrUpdateAsync(tournament);
        }
    }
}