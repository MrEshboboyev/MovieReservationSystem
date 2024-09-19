namespace MovieReservationSystem.Application.Common.Models
{
    public class TicketPurchaseRequestModel
    {
        public Guid TicketId { get; set; }
        public string StripeToken { get; set; }
    }
}
