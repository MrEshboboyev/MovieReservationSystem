namespace MovieReservationSystem.Application.DTOs
{
    public class ReserveSeatDTO
    {
        public string UserId { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid SeatId { get; set; }
    }
}
