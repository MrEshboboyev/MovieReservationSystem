using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface ITicketService
    {
        // Purchase Ticket
        Task<TicketDTO> PurchaseTicketAsync(TicketPurchaseRequestDTO ticketPurchaseRequestDTO);

        // View Ticket Details
        Task<TicketDTO> GetTicketDetailsAsync(Guid ticketId);

        // View all purchased tickets for a user
        Task<IEnumerable<TicketDTO>> GetUserTicketsAsync(string userId);

        // Optional : Request refund for a ticket
        Task<bool> RequestRefundAsync(Guid ticketId);
    }
}
