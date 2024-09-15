namespace MovieReservationSystem.Application.DTOs
{
    public class SeatDTO
    {
        public Guid SeatId { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }   
    }
}
