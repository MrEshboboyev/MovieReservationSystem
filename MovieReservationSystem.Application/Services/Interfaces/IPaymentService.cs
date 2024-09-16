using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDTO> CreatePaymentAsync(Guid ticketId, PaymentRequestDTO paymentRequestDTO);
        Task<PaymentDTO> GetPaymentDetails(Guid paymentId);
        Task<bool> RefundPaymentAsync(Guid paymentId); // Optional, if refunded is supported
    }
}
