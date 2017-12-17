using Nibo.SpaceJam.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.Services.Abstractions
{
    /// <summary>
    /// Features for tournament results management
    /// </summary>
    public interface ITournamentResultService : IService<TournamentResultModel>
    {
        /// <summary>
        /// Register tournament quarter finals
        /// </summary>
        /// <param name="tournamentId">Id of tournament</param>
        /// <param name="tournamentQuarterFinals">Quarter finals informations</param>
        /// <returns></returns>
        Task RegisterQuarterFinals(string tournamentId, TournamentQuarterFinalsMatchModel tournamentQuarterFinals);

        /// <summary>
        /// Register tournament semi finals
        /// </summary>
        /// <param name="tournamentId">Id of tournament</param>
        /// <param name="tournamentSemiFinals">Semi finals informations</param>
        /// <returns></returns>
        Task RegisterSemiFinals(string tournamentId, TournamentSemiFinalsMatchModel tournamentSemiFinals);

        /// <summary>
        /// Register tournament final
        /// </summary>
        /// <param name="tournamentId">Id of tournament</param>
        /// <param name="tournamentFinal">Final informations</param>
        /// <returns></returns>
        Task RegisterFinal(string tournamentId, TournamentFinalMatchModel tournamentFinal);

        /// <summary>
        /// Register tournament winner
        /// </summary>
        /// <param name="tournamentId">Id of tournament</param>
        /// <param name="teamId">Id of winner team</param>
        /// <returns></returns>
        Task Registerwinner(string tournamentId, string teamId);
    }
}
