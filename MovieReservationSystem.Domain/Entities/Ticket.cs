namespace MovieReservationSystem.Domain.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public Guid SeatId { get; set; }
        public Seat Seat { get; set; }

        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
