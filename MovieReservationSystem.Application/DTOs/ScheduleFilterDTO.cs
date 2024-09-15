namespace MovieReservationSystem.Application.DTOs
{
    public class ScheduleFilterDTO
    {
        public Guid? MovieId { get; set; }
        public Guid? TheaterId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
