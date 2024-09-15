namespace MovieReservationSystem.Application.DTOs
{
    public class UpdateScheduleDTO
    {
        public DateTime ShowTime { get; set; }
        public Guid TheaterId { get; set; }
    }
}
