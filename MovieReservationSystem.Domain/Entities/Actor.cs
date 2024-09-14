namespace MovieReservationSystem.Domain.Entities
{
    public class Actor
    {
        public Guid ActorId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        // relation
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
