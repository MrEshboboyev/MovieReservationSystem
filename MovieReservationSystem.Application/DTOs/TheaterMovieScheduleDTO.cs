namespace MovieReservationSystem.Application.DTOs
{
    public class TheaterMovieScheduleDTO
    {
        public Guid ScheduleId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ShowTime { get; set; }
    }
}
