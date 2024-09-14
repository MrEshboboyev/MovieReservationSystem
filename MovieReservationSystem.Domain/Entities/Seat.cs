namespace MovieReservationSystem.Domain.Entities
{
    public class Seat
    {
        public Guid SeatId { get; set; }
        public Guid TheaterId { get; set; }
        public Theater Theater { get; set; }
        public string SeatNumber { get; set; }

        // relations
        public ICollection<Ticket> Tickets { get; set; }
    }
}
