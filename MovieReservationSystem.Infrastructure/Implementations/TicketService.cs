using AutoMapper;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Domain.Enums;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class TicketService(IUnitOfWork unitOfWork, IMapper mapper,
        IPaymentService paymentService) : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IPaymentService _paymentService = paymentService;

        public async Task<TicketPurchaseResponseDTO> PurchaseTicketAsync(TicketPurchaseRequestDTO ticketPurchaseRequestDTO)
        {
            try
            {
                // get the ticket (by seat number && schedule.theaterId)
                var ticketFromDb = _unitOfWork.Ticket.Get(
                    filter: s => s.TicketId.Equals(ticketPurchaseRequestDTO.TicketId)
                    );

                // if seat is null or seat Tickets schedule is not find, return false
                if (ticketFromDb == null)
                {
                    return new TicketPurchaseResponseDTO()
                    {
                        Message = "Ticket is not found!",
                        Success = false
                    };
                }


                // checking status is purchased
                if (ticketFromDb.Status.Equals(TicketStatus.Purchased))
                {
                    return new TicketPurchaseResponseDTO()
                    {
                        Message = "The ticket is already paid.",
                        Success = false
                    };
                }

                // Handle Payment using the service
                var processedPayment = await _paymentService.CreatePaymentAsync(
                    ticketFromDb.TicketId, ticketPurchaseRequestDTO.PaymentRequestDTO
                    );

                // associate the payment with the ticket
                ticketFromDb.PaymentId = processedPayment.PaymentId;
                ticketFromDb.Payment = _mapper.Map<Payment>(processedPayment);
                ticketFromDb.Status = TicketStatus.Purchased;

                // save the updated ticket with payment
                _unitOfWork.Ticket.Update(ticketFromDb);

                await _unitOfWork.Save();

                // return the response dto
                return new TicketPurchaseResponseDTO()
                {
                    Success = true,
                    Ticket = _mapper.Map<TicketDTO>(ticketFromDb),
                    Payment = processedPayment,
                    Message = "Ticket purchased successfully!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TicketDTO> GetTicketDetailsAsync(Guid ticketId)
        {
            try
            {
                // getting this ticket
                var ticketFromDb = _unitOfWork.Ticket.Get(
                    filter: t => t.TicketId.Equals(ticketId),
                    includeProperties: "Seat,Schedule,Schedule.Movie,Schedule.Theater,Payment" 
                    )?? throw new Exception("Ticket not found!");

                return _mapper.Map<TicketDTO>(ticketFromDb);    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TicketDTO>> GetUserTicketsAsync(string userId)
        {
            try
            {
                // getting this ticket
                var ticketFromDb = _unitOfWork.Ticket.GetAll(
                    filter: t => t.UserId.Equals(userId),
                    includeProperties: "Seat,Schedule,Schedule.Movie,Schedule.Theater,Payment"
                    ) ?? throw new Exception("User tickets not found!");

                return _mapper.Map<IEnumerable<TicketDTO>>(ticketFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> RequestRefundAsync(Guid ticketId)
        {
            try
            {
                // getting this ticket from db
                var ticketFromDb = _unitOfWork.Ticket.Get(t => t.TicketId.Equals(ticketId))
                    ?? throw new Exception("Ticket not found!");

                // ticket payment null or payment status is not confirmed
                if (ticketFromDb.Payment == null || ticketFromDb.Payment.Status != "Confirmed!")
                    return false; // Cannot refund unconfirmed or unpaid tickets

                var isRefunded = await _paymentService.RefundPaymentAsync(ticketFromDb.PaymentId);
                if (isRefunded) // refund this payment
                {
                    // update ticket payment status to "Refunded"
                    ticketFromDb.Payment.Status = "Refunded";
                    
                    // update ticket and save db
                    _unitOfWork.Ticket.Update(ticketFromDb);
                    await _unitOfWork.Save();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
