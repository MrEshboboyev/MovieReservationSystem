using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Guid ticketId, PaymentRequestDTO paymentRequestDTO);
        Task<PaymentDTO> GetPaymentDetails(Guid paymentId);
        Task<bool> RefundPaymentAsync(Guid paymentId); // Optional, if refunded is supported
    }
}
