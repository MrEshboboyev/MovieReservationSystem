using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Common.Utility;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;

namespace MovieReservationSystem.Presentation.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController(IActorService actorService) : ControllerBase
    {
        private readonly IActorService _actorService = actorService;

        [HttpGet("get-all-actors")]
        public async Task<IActionResult> GetAllActors()
        {
            try
            {
                return Ok(await _actorService.GetAllActorsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-movie-actors")]
        public async Task<IActionResult> GetMovieActors(Guid movieId)
        {
            try
            {
                return Ok(await _actorService.GetActorsByMovieAsync(movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-actor-by-Id")]
        public async Task<IActionResult> GetActorById(Guid actorId)
        {
            try
            {
                return Ok(await _actorService.GetActorByIdAsync(actorId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-actor")]
        public async Task<IActionResult> CreateActor([FromBody] CreateActorDTO createActorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _actorService.CreateActorAsync(createActorDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-actor")]
        public async Task<IActionResult> UpdateActor(Guid actorId, [FromBody] UpdateActorDTO updateActorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!actorId.Equals(updateActorDTO.ActorId)) 
                return BadRequest("ActorId and UpdateActor.ActorId must be equal!");

            try
            {
                return Ok(await _actorService.UpdateActorAsync(updateActorDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-actor")]
        public async Task<IActionResult> DeleteActor(Guid actorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _actorService.DeleteActorAsync(actorId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
