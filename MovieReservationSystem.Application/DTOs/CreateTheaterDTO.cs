namespace MovieReservationSystem.Application.DTOs
{
    public class CreateTheaterDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<CreateSeatDTO> Seats { get; set; }
    }
}
