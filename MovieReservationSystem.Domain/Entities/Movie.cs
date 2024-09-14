namespace MovieReservationSystem.Domain.Entities
{
    public class Movie
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }

        // relations
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
