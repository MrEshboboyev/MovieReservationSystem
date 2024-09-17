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
    public class MovieController : ControllerBase
    {
        // inject IMovieService
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Customer}")]
        [HttpGet("get-all-movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                return Ok(await _movieService.GetAllMoviesWithDetailsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Customer}")]
        [HttpGet("get-movie")]
        public async Task<IActionResult> GetMovieById(Guid movieId)
        {
            try
            {
                return Ok(await _movieService.GetMovieByIdAsync(movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-movie")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDTO createMovieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _movieService.CreateMovieAsync(createMovieDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update-movie")]
        public async Task<IActionResult> UpdateMovie(Guid movieId, [FromBody] UpdateMovieDTO updateMovieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _movieService.UpdateMovieAsync(movieId, updateMovieDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-movie")]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            try
            {
                return Ok(await _movieService.DeleteMovieAsync(movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
