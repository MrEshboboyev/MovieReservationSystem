namespace MovieReservationSystem.Application.DTOs
{
    public class TicketPurchaseResponseDTO
    {
        public TicketDTO Ticket { get; set; }
        public PaymentDTO Payment { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } 
    }
}
