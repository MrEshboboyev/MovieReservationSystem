using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;
using Stripe;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class StripePaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public StripePaymentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<PaymentDTO> CreatePaymentAsync(Guid ticketId, PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                // get ticket with relations
                var ticketFromDb = _unitOfWork.Ticket.Get(
                    filter: t => t.TicketId.Equals(ticketId),
                    includeProperties: "Schedule.Movie, Seats"
                    ) ?? throw new Exception("Ticket not found!");

                // prepare payment amount
                var paymentAmount = ticketFromDb.Price;

                // new ChargeCreateOptions
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(paymentAmount * 100), // Stripe handles amount in cents
                    Currency = "usd",
                    Description = $"Payment for movie ticket : " +
                    $"{ticketFromDb.Schedule.Movie.Title}; " +
                    $"Seat : {ticketFromDb.Seat.SeatNumber}",
                    Source = paymentRequestDTO.StripeToken // stripe token from the frontend
                };

                // create charge service interface
                var service = new ChargeService();

                // payment process
                try
                {
                    var charge = await service.CreateAsync(options);

                    if (charge.Status.Equals("succeeded"))
                    {
                        // create new payment for db
                        var paymentForDb = new Payment
                        {
                            StripePaymentId = charge.Id,
                            Amount = paymentAmount,
                            PaymentDate = DateTime.UtcNow,
                            Status = charge.Status,
                            TicketId = ticketId,
                            UserId = paymentRequestDTO.UserId
                        };

                        // adding payment to db
                        _unitOfWork.Payment.Add(paymentForDb);
                        await _unitOfWork.Save();

                        // mapping and return
                        return _mapper.Map<PaymentDTO>(paymentForDb);
                    }

                    throw new Exception("Payment failed!");
                }
                catch (StripeException ex)
                {
                    throw new Exception($"Stripe error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaymentDTO> GetPaymentDetails(Guid paymentId)
        {
            try
            {
                // getting payment from db
                var paymentFromDb = _unitOfWork.Payment.Get(
                    filter: p => p.PaymentId.Equals(paymentId)
                    ) ?? throw new Exception("Payment not found!");

                return _mapper.Map<PaymentDTO>(paymentFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RefundPaymentAsync(Guid paymentId)
        {
            try
            {
                // getting payment from db
                var paymentFromDb = _unitOfWork.Payment.Get(
                    filter: p => p.PaymentId.Equals(paymentId)
                    ) ?? throw new Exception("Payment not found!");

                // Refund Service interface
                var service = new RefundService();
                var options = new RefundCreateOptions
                {
                    Charge = paymentFromDb.StripePaymentId
                };

                // refunding process
                try
                {
                    var refund = await service.CreateAsync(options);

                    // update the payment status and save
                    paymentFromDb.Status = "refunded";
                    _unitOfWork.Payment.Update(paymentFromDb);
                    await _unitOfWork.Save();

                    return true;
                }
                catch (StripeException ex)
                {
                    throw new Exception($"Stripe refund error : {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
