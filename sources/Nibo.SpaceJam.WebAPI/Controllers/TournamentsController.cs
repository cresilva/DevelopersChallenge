using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nibo.SpaceJam.Models;
using Nibo.SpaceJam.Services.Abstractions;
using Nibo.SpaceJam.Services.Abstractions.ValueObjects;

namespace Nibo.SpaceJam.WebAPI.Controllers
{
    /// <summary>
    /// Tournament endpoints
    /// </summary>
    [Produces("application/json")]
    [Route("v1/tournaments")]
    public class TournamentsController : Controller
    {
        private readonly ITournamentService _tournamentService;

        #region Ctor
        /// <summary>
        /// Initialize tournament endpoints
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="tournamentService">Injected instance of tournament service</param>
        public TournamentsController(IHttpContextAccessor httpContextAccessor
            , ITournamentService tournamentService)
        {
            this._tournamentService = tournamentService;

            //Identify request user for operation loggs
            this._tournamentService.RequestIdentification = new RequestIdentification()
            {
                Origin = httpContextAccessor.HttpContext?.Request.Headers["x-runtime-platform"].FirstOrDefault(),
                UserAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress.ToString(),
                UserLogin = httpContextAccessor.HttpContext?.Request.Headers["x-request-user"].FirstOrDefault()
            };
        }
        #endregion

        #region Tournament endpoints

        /// <summary>
        /// Get all registered tornaments
        /// </summary>
        /// <returns>List of tornaments</returns>
        [HttpGet]
        [ProducesResponseType(typeof(TournamentModel[]), 200)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await this._tournamentService.ListAllAsync());
        }

        /// <summary>
        /// Get tornament by registration id
        /// </summary>
        /// <param name="id">Id of tornament</param>
        /// <returns>Tornament informations</returns>
        /// <response code="200">Returns when tournament has been found</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TournamentModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await this._tournamentService.GetByIdAsync(id, "TournamentsController_GetAsync_NotFound".Translate()));
        }

        /// <summary>
        /// Register a new tornament
        /// </summary>
        /// <param name="payload">Tornament informations</param>
        /// <response code="201">Returns when team has been created</response>
        /// <response code="400">If payload has invalid data</response>  
        [HttpPost]
        [ProducesResponseType(typeof(TournamentModel), 201)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostAsync([FromBody]TournamentModel payload)
        {
            var createdTournament = await this._tournamentService.CreateOrUpdateAsync(payload);

            return CreatedAtAction(nameof(GetAsync), new { id = createdTournament.Id }, createdTournament);
        }

        /// <summary>
        /// Update tornament informations
        /// </summary>
        /// <param name="payload">Tornament informations</param>
        /// <response code="200">Returns when tournament has been updated</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        /// <response code="400">If payload has invalid data</response>  
        [HttpPut()]
        [ProducesResponseType(typeof(TournamentModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PutAsync([FromBody]TournamentModel payload)
        {
            return Ok(await this._tournamentService.CreateOrUpdateAsync(payload));
        }

        /// <summary>
        /// Delete tournament
        /// </summary>
        /// <param name="id">Id of tournament</param>
        /// <returns></returns>
        /// <response code="200">Returns when tournament has been deleted</response>
        /// <response code="404">If the id of tournament is invalid</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this._tournamentService.DeleteAsync(id, "TournamentsController_GetAsync_NotFound".Translate());

            return Ok();
        }
        #endregion
    }
}