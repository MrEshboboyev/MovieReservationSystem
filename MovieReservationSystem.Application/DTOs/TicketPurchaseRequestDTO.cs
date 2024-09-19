namespace MovieReservationSystem.Application.DTOs
{
    public class TicketPurchaseRequestDTO
    {
        public Guid TicketId { get; set; }
        public PaymentRequestDTO PaymentRequestDTO { get; set; }
    }
}
