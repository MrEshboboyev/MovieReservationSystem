namespace MovieReservationSystem.Application.Common.Models
{
    public class TicketPurchaseRequestModel
    {
        public Guid ScheduleId { get; set; }
        public string SeatNumber { get; set; }
    }
}
