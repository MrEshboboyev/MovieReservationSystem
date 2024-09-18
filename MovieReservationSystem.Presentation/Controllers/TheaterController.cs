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

        // Seat Management
        [HttpGet("get-theater-seats")]
        public async Task<IActionResult> GetTheaterSeats(Guid theaterId)
        {
            try
            {
                return Ok(await _theaterService.GetSeatsByTheaterIdAsync(theaterId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut("add-seat-to-theater")]
        public async Task<IActionResult> AddSeatToTheater(Guid theaterId, [FromBody] CreateSeatDTO createSeatDTO)
        {
            try
            {
                return Ok(await _theaterService.AddSeatToTheaterAsync(theaterId, createSeatDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut("update-seat-to-theater")]
        public async Task<IActionResult> UpdateSeatToTheater(Guid theaterId, Guid seatId, [FromBody] UpdateSeatDTO updateSeatDTO)
        {
            try
            {
                return Ok(await _theaterService.UpdateSeatToTheaterAsync(theaterId, seatId, updateSeatDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut("remove-seat-from-theater")]
        public async Task<IActionResult> RemoveSeatFromTheater(Guid theaterId, Guid seatId)
        {
            try
            {
                return Ok(await _theaterService.RemoveSeatFromTheaterAsync(theaterId, seatId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
