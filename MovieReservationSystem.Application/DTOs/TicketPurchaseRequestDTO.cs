namespace MovieReservationSystem.Application.DTOs
{
    public class TicketPurchaseRequestDTO
    {
        public Guid ScheduleId { get; set; }
        public string SeatNumber { get; set; }
        public PaymentRequestDTO PaymentRequestDTO { get; set; }
    }
}
