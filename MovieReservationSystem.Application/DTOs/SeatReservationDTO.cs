namespace MovieReservationSystem.Application.DTOs
{
    public class SeatReservationDTO
    {
        public Guid ReservationId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string MovieTitle { get; set; }
        public string TheaterName { get; set; }
        public string SeatNumber { get; set; }
        public DateTime ShowTime { get; set; }
        public decimal Price { get; set; }
    }
}
