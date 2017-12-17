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
    /// Team endpoints
    /// </summary>
    [Produces("application/json")]
    [Route("v1/teams")]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        /// <summary>
        /// Initialize team endpoints
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="teamService">Injected instance of team service</param>
        public TeamsController(IHttpContextAccessor httpContextAccessor, ITeamService teamService)
        {
            this._teamService = teamService;

            //Identify request user for operation loggs
            this._teamService.RequestIdentification = new RequestIdentification()
            {
                Origin = httpContextAccessor.HttpContext?.Request.Headers["x-runtime-platform"].FirstOrDefault(),
                UserAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress.ToString(),
                UserLogin = httpContextAccessor.HttpContext?.Request.Headers["x-request-user"].FirstOrDefault()
            };
        }

        /// <summary>
        /// Get all registeres teams
        /// </summary>
        /// <returns>List of teams</returns>
        [HttpGet]
        [ProducesResponseType(typeof(TeamModel[]), 200)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await this._teamService.ListAllAsync());
        }

        /// <summary>
        /// Get team by registration id
        /// </summary>
        /// <param name="id">Id of team</param>
        /// <returns>Team informations</returns>
        /// <response code="200">Returns when team has been found</response>
        /// <response code="404">If the id of team is invalid</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TeamModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)) return BadRequest();

            return Ok(await this._teamService.GetByIdAsync(id, "TeamsController_GetAsync_NotFound".Translate()));
        }

        /// <summary>
        /// Register a new team
        /// </summary>
        /// <param name="payload">Team informations</param>
        /// <response code="201">Returns when team has been created</response>
        /// <response code="400">If payload has invalid data</response>  
        [HttpPost]
        [ProducesResponseType(typeof(TeamModel), 201)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PostAsync([FromBody]TeamModel payload)
        {
            var createdTeam = await this._teamService.CreateOrUpdateAsync(payload);

            return CreatedAtAction(nameof(GetAsync), new { id = createdTeam.Id }, createdTeam);
        }

        /// <summary>
        /// Update team informations
        /// </summary>
        /// <param name="payload">Team informations</param>
        /// <response code="200">Returns when team has been updated</response>
        /// <response code="404">If the id of team is invalid</response>  
        /// <response code="400">If payload has invalid data</response>  
        [HttpPut()]
        [ProducesResponseType(typeof(TeamModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        public async Task<IActionResult> PutAsync([FromBody]TeamModel payload)
        {
            return Ok(await this._teamService.CreateOrUpdateAsync(payload));
        }

        /// <summary>
        /// Delete team
        /// </summary>
        /// <response code="200">Returns when team has been deleted</response>
        /// <response code="404">If the id of team is invalid</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this._teamService.DeleteAsync(id, "TeamsController_GetAsync_NotFound".Translate());

            return Ok();
        }
    }
}