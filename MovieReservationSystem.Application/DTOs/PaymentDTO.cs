namespace MovieReservationSystem.Application.DTOs
{
    public class PaymentDTO
    {
        public Guid PaymentId { get; set; }
        public string StripePaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}
