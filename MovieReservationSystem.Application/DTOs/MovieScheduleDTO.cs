namespace MovieReservationSystem.Application.DTOs
{
    public class MovieScheduleDTO
    {
        public int ScheduleId { get; set; }
        public DateTime ShowTime { get; set; }
        public string TheaterName { get; set; }
        public decimal Price { get; set; }
    }
}
