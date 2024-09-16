namespace MovieReservationSystem.Application.DTOs
{
    public class TicketDTO
    {
        public Guid TicketId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string SeatNumber { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ShowTime { get; set; }
        public string TheaterName { get; set; }
        public string Status { get; set; }  // e.g. Confirmed, Cancelled, Refunded
    }
}
