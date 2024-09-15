namespace MovieReservationSystem.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public string StripePaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }


        // relations (user and ticket)
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
