namespace MovieReservationSystem.Application.DTOs
{
    public class ActorDetailedDTO
    {
        public Guid ActorId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public ICollection<string> Movies { get; set; } // Movie titles
    }
}
