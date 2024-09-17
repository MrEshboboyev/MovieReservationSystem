using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Common.Utility;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;

namespace MovieReservationSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IScheduleService scheduleService) : ControllerBase
    {
        private readonly IScheduleService _scheduleService = scheduleService;

        [HttpGet("get-available-schedules")]
        public async Task<IActionResult> GetAvailableSchedules([FromQuery] ScheduleFilterDTO scheduleFilterDTO)
        {
            try
            {
                return Ok(await _scheduleService.GetAvailableSchedulesAsync(scheduleFilterDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-schedules-movie")]
        public async Task<IActionResult> GetMovieSchedules(Guid movieId)
        {
            try
            {
                return Ok(await _scheduleService.GetSchedulesForMovieAsync(movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-schedules-theater")]
        public async Task<IActionResult> GetTheaterSchedules(Guid theaterId)
        {
            try
            {
                return Ok(await _scheduleService.GetSchedulesForTheaterAsync(theaterId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-schedules-in-range")]
        public async Task<IActionResult> GetSchedulesInRange(DateTime starDate, DateTime endDate)
        {
            try
            {
                return Ok(await _scheduleService.GetSchedulesInRangeAsync(starDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-schedule-by-id")]
        public async Task<IActionResult> GetScheduleById(Guid scheduleId)
        {
            try
            {
                return Ok(await _scheduleService.GetScheduleByIdAsync(scheduleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost("create-schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDTO createScheduleDTO)
        {
            try
            {
                return Ok(await _scheduleService.CreateScheduleAsync(createScheduleDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut("update-schedule")]
        public async Task<IActionResult> UpdateSchedule(Guid scheduleId, [FromBody] UpdateScheduleDTO updateScheduleDTO)
        {
            try
            {
                return Ok(await _scheduleService.UpdateScheduleAsync(scheduleId, updateScheduleDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpDelete("delete-schedule")]
        public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
        {
            try
            {
                await _scheduleService.DeleteScheduleAsync(scheduleId);
                return Ok("Deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
