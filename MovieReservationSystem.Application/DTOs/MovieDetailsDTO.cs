namespace MovieReservationSystem.Application.DTOs
{
    public class MovieDetailsDTO
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }

        // relations
        public ICollection<ActorBasicDTO> Actors { get; set; }
        public ICollection<MovieScheduleDTO> Schedules { get; set; }
    }
}
