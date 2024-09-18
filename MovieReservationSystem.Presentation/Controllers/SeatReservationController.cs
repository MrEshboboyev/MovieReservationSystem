using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Common.Models;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using System.Security.Claims;

namespace MovieReservationSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatReservationController(ISeatReservationService seatReservationService) : ControllerBase
    {
        private readonly ISeatReservationService _seatReservationService = seatReservationService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Login required!");
        #endregion

        [HttpGet("get-available-seats")]
        public async Task<IActionResult> GetAvailableSeats(Guid scheduleId)
        {
            try
            {
                return Ok(await _seatReservationService.GetAvailableSeatsAsync(scheduleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user-reservations")]
        public async Task<IActionResult> GetUserReservations()
        {
            try
            {
                return Ok(await _seatReservationService.GetUserReservationsAsync(GetUserId()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-schedule-reservations")]
        public async Task<IActionResult> GetScheduleReservations(Guid scheduleId)
        {
            try
            {
                return Ok(await _seatReservationService.GetReservationsForScheduleAsync(scheduleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("check-available-seat")]
        public async Task<IActionResult> GetSeatAvailability(Guid seatId, Guid scheduleId)
        {
            try
            {
                return Ok(await _seatReservationService.IsSeatAvailableAsync(scheduleId, seatId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reserve-seat")]
        public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatModel reserveSeatModel)
        {
            try
            {
                // prepare ReserveSeatDTO
                ReserveSeatDTO reserveSeatDTO = new()
                {
                    ScheduleId = reserveSeatModel.ScheduleId,
                    SeatId = reserveSeatModel.SeatId,
                    UserId = GetUserId()
                };

                return Ok(await _seatReservationService.ReserveSeatAsync(reserveSeatDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cancel-seat-reservation")]
        public async Task<IActionResult> CancelSeatReservation(Guid ticketId)
        {
            try
            {
                await _seatReservationService.CancelSeatReservationAsync(GetUserId(), ticketId);
                return Ok("Cancelled successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
