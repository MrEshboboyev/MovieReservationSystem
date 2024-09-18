using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Application.Common.Models;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using System.Security.Claims;

namespace MovieReservationSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(ITicketService ticketService) : ControllerBase
    {
        private readonly ITicketService _ticketService = ticketService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required!");
        #endregion

        [HttpPost("purchase-ticket")]
        public async Task<IActionResult> PurchaseTicket([FromBody] TicketPurchaseRequestModel ticketPurchaseRequestModel)
        {
            try
            {
                // prepare ticketPurchaseRequestDTO
                TicketPurchaseRequestDTO ticketPurchaseRequestDTO = new()
                {
                    ScheduleId = ticketPurchaseRequestModel.ScheduleId,
                    SeatNumber = ticketPurchaseRequestModel.SeatNumber,
                    PaymentRequestDTO = new()
                    {
                        UserId = GetUserId(),
                        StripeToken = ""
                    }
                };

                return Ok(await _ticketService.PurchaseTicketAsync(ticketPurchaseRequestDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
