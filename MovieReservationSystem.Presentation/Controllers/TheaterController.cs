using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Common.Utility;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;

namespace MovieReservationSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController(ITheaterService theaterService) : ControllerBase
    {
        private readonly ITheaterService _theaterService = theaterService;

        [HttpGet("get-all-theaters")]
        public async Task<IActionResult> GetAllTheaters()
        {
            try
            {
                return Ok(await _theaterService.GetAllTheatersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-theater-by-id")]
        public async Task<IActionResult> GetTheaterById(Guid theaterId)
        {
            try
            {
                return Ok(await _theaterService.GetTheaterByIdAsync(theaterId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost("create-theaters")]
        public async Task<IActionResult> CreateTheater([FromBody] CreateTheaterDTO createTheaterDTO)
        {
            try
            {
                return Ok(await _theaterService.CreateTheaterAsync(createTheaterDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut("update-theater")]
        public async Task<IActionResult> UpdateTheater(Guid theaterId, [FromBody] UpdateTheaterDTO updateTheaterDTO)
        {
            try
            {
                return Ok(await _theaterService.UpdateTheaterAsync(theaterId, updateTheaterDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-theater")]
        public async Task<IActionResult> RemoveTheater(Guid theaterId)
        {
            try
            {
                return Ok(await _theaterService.DeleteTheaterAsync(theaterId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
