namespace MovieReservationSystem.Application.DTOs
{
    public class TheaterDTO
    {
        public Guid TheaterId { get; set; }
        public string Name { get; set; }    
        public string Location { get; set; }
        public IEnumerable<SeatDTO> Seats { get; set; }
    }
}
