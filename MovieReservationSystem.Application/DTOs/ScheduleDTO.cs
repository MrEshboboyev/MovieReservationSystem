namespace MovieReservationSystem.Application.DTOs
{
    public class ScheduleDTO
    {
        public Guid ScheduleId { get; set; }
        public Guid MovieId { get; set; }
        public string MovieTitle { get; set; }
        public Guid TheaterId { get; set; }
        public string TheaterName { get; set; }
        public DateTime ShowTime { get; set; }
        public decimal Price { get; set; }
    }
}
