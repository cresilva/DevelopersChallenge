using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nibo.SpaceJam.Infraestructure;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Services.Abstractions;
using Nibo.SpaceJam.Services.Abstractions.ValueObjects;

namespace Nibo.SpaceJam.WebAPI.Controllers
{
    /// <summary>
    /// Tournament results endpoints
    /// </summary>
    [Produces("application/json")]
    [Route("v1/tournament-results")]
    public class TournamentResultsController : Controller
    {
        private readonly ITournamentResultService _tournamentResultService;
        private readonly ITournamentService _tournamentService;

        #region Ctor
        /// <summary>
        /// Initialize tournament results endpoints
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="tournamentResultService">Injected instance of tournament result service</param>
        /// <param name="tournamentService">Injected instance of tournament service</param>
        public TournamentResultsController(IHttpContextAccessor httpContextAccessor
            , ITournamentResultService tournamentResultService
            , ITournamentService tournamentService)
        {
            this._tournamentResultService = tournamentResultService;
            this._tournamentService = tournamentService;

            //Identify request user for operation loggs
            this._tournamentResultService.RequestIdentification = new RequestIdentification()
            {
                Origin = httpContextAccessor.HttpContext?.Request.Headers["x-runtime-platform"].FirstOrDefault(),
                UserAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress.ToString(),
                UserLogin = httpContextAccessor.HttpContext?.Request.Headers["x-request-user"].FirstOrDefault()
            };

            this._tournamentService.RequestIdentification = this._tournamentResultService.RequestIdentification;
        }
        #endregion

        #region Tournament result endpoints

        /// <summary>
        /// Get complete tournament results
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <returns></returns>
        /// <response code="200">Returns when result has been found</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TournamentResultModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResultAsync(string id)
        {
            return Ok(await this._tournamentResultService.GetSingleAsync(x => x.Tournament.Equals(id), "TournamentResultsController_GetResultAsync_NotFound".Translate()));
        }

        /// <summary>
        /// Get quarter finals results
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <returns></returns>
        /// <response code="200">Returns when result has been found</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpGet("{id}/quarter-finals")]
        [ProducesResponseType(typeof(TournamentQuarterFinalsMatchModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResultForQuarterFinalsAsync(string id)
        {
            var results = await this._tournamentResultService.GetSingleAsync(x => x.Tournament.Equals(id), "TournamentResultsController_GetResultAsync_NotFound".Translate());

            if (results.QuarterFinals == null)
                return NotFound("TournamentResultsController_GetResultForQuarterFinalsAsync_NotFound".Translate());

            return Ok(results.QuarterFinals);
        }

        /// <summary>
        /// Set tournament quarter final teams
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <param name="payload">Quarter final match informations</param>
        /// <response code="200">Returns when tournament quarter final teams has been defined</response>
        /// <response code="404">If the id of tournament or id of teams is invalid</response>  
        /// <response code="400">If payload has invalid data</response>  
        [HttpPatch("{id}/quarter-finals")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PatchQuarterFinalAsync(string id, [FromBody]TournamentQuarterFinalsMatchModel payload)
        {
            await this._tournamentResultService.RegisterQuarterFinals(id, payload);

            return Ok();
        }

        /// <summary>
        /// Get semi finals results
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <returns></returns>
        /// <response code="200">Returns when result has been found</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpGet("{id}/semi-finals")]
        [ProducesResponseType(typeof(TournamentSemiFinalsMatchModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResultForSemiFinalsAsync(string id)
        {
            var results = await this._tournamentResultService.GetSingleAsync(x => x.Tournament.Equals(id), "TournamentResultsController_GetResultAsync_NotFound".Translate());

            if (results.SemiFinals == null)
                return NotFound("TournamentResultsController_GetResultForSemiFinalsAsync_NotFound".Translate());

            return Ok(results.SemiFinals);
        }

        /// <summary>
        /// Set tournament semi final teams
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <param name="payload">Semi final match informations</param>
        /// <response code="200">Returns when tournament semi final teams has been defined</response>
        /// <response code="404">If the id of tournament or id of teams is invalid</response>  
        /// <response code="400">If payload has invalid data</response>  
        [HttpPatch("{id}/semi-finals")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PatchSemiFinalAsync(string id, [FromBody]TournamentSemiFinalsMatchModel payload)
        {
            await this._tournamentResultService.RegisterSemiFinals(id, payload);

            return Ok();
        }

        /// <summary>
        /// Get final results
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <returns></returns>
        /// <response code="200">Returns when result has been found</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpGet("{id}/final")]
        [ProducesResponseType(typeof(TournamentFinalMatchModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResultForFinalAsync(string id)
        {
            var results = await this._tournamentResultService.GetSingleAsync(x => x.Tournament.Equals(id), "TournamentResultsController_GetResultAsync_NotFound".Translate());

            if (results.Final == null)
                return NotFound("TournamentResultsController_GetResultForFinalAsync_NotFound".Translate());

            return Ok(results.Final);
        }

        /// <summary>
        /// Set tournament final teams
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <param name="payload">Final match informations</param>
        /// <response code="200">Returns when tournament final teams has been defined</response>
        /// <response code="404">If the id of tournament or id of teams is invalid</response>  
        /// <response code="400">If payload has invalid data</response>  
        [HttpPatch("{id}/final")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PatchFinalAsync(string id, [FromBody]TournamentFinalMatchModel payload)
        {
            await this._tournamentResultService.RegisterFinal(id, payload);

            return Ok();
        }

        /// <summary>
        /// Get tournament winner
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <returns>Team winner</returns>
        /// <response code="200">Returns when tournament has a winner</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpGet("{id}/winner")]
        [ProducesResponseType(typeof(TeamModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetWinnerAsync(string id)
        {
            var results = await this._tournamentResultService.GetSingleAsync(x => x.Tournament.Equals(id), "TournamentResultsController_GetResultAsync_NotFound".Translate());

            if (results.Winner == null)
                return NotFound("TournamentResultsController_GetWinnerAsync_NotFound".Translate());

            return Ok(results.Winner);
        }

        /// <summary>
        /// Set tournament winner
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <param name="payload">Winner team</param>
        /// <response code="200">Returns when tournament winner has been defined</response>
        /// <response code="404">If the id of tournament or id of winner team is invalid</response>  
        [HttpPatch("{id}/winner")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PatchWinnerAsync(string id, [FromBody]TeamModel payload)
        {
            await this._tournamentResultService.Registerwinner(id, payload.Id);

            return Ok();
        }

        #endregion
    }
}