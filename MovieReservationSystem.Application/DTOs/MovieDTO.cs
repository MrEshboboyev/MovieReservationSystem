namespace MovieReservationSystem.Application.DTOs
{
    public class MovieDTO
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
