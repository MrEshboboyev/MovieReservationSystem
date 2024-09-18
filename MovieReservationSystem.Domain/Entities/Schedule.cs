namespace MovieReservationSystem.Domain.Entities
{
    public class Schedule
    {
        public Guid ScheduleId { get; set; }
        public DateTime ShowTime { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid TheaterId { get; set; }
        public Theater Theater { get; set; }

        // Add a base price for all tickets for this schedule
        public decimal Price { get; set; }

        // relations
        public ICollection<Ticket> Tickets { get; set; }
    }
}
