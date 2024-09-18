namespace MovieReservationSystem.Application.DTOs
{
    public class ScheduleDetailsDTO
    {
        public Guid ScheduleId { get; set; }
        public string MovieTitle { get; set; }
        public string TheaterName { get; set; }
        public DateTime ShowTime { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<SeatDTO> AvailableSeats { get; set; }
    }
}
