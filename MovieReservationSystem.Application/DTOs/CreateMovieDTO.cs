namespace MovieReservationSystem.Application.DTOs
{
    public class CreateMovieDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Guid> ActorIds { get; set; } // Associate actors by IDs
    }
}
