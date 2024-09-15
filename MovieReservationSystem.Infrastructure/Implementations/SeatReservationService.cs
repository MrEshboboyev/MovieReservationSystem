using AutoMapper;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class SeatReservationService(IUnitOfWork unitOfWork, IMapper mapper) : ISeatReservationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


        public async Task<IEnumerable<SeatDTO>> GetAvailableSeatsAsync(Guid scheduleId)
        {
            try
            {
                // get this schedule with theater seats and tickets
                var scheduleFromDb = _unitOfWork.Schedule.Get(
                    filter: c => c.ScheduleId.Equals(scheduleId),
                    includeProperties: "Theater.Seats,Tickets");

                // available seats : tickets seatId is not assigned
                var availableSeats = scheduleFromDb.Theater.Seats
                    .Where(seat => !scheduleFromDb.Tickets.Any(
                        ticket => ticket.SeatId.Equals(seat.SeatId)));

                return _mapper.Map<IEnumerable<SeatDTO>>(availableSeats);   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SeatReservationDTO>> GetReservationsForScheduleAsync(Guid scheduleId)
        {
            try
            {
                // get tickets from db with relations
                var ticketsFromDb = _unitOfWork.Ticket.GetAll(
                    filter: t => t.ScheduleId.Equals(scheduleId),
                    includeProperties: "Schedule.Movie, Schedule.Theater,Seat,User"
                    );

                return _mapper.Map<IEnumerable<SeatReservationDTO>>(ticketsFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SeatReservationDTO>> GetUserReservationsAsync(string userId)
        {
            try
            {
                // get tickets from db with relations
                var ticketsFromDb = _unitOfWork.Ticket.GetAll(
                    filter: t => t.UserId.Equals(userId),
                    includeProperties: "Schedule.Movie, Schedule.Theater,Seat"
                    );

                return _mapper.Map<IEnumerable<SeatReservationDTO>>(ticketsFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsSeatAvailableAsync(Guid scheduleId, Guid seatId)
        {
            try
            {
                // get tickets and checking this schedule and seat is available
                return !_unitOfWork.Ticket.Any(
                    t => t.ScheduleId.Equals(scheduleId) && t.SeatId.Equals(seatId)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeatReservationDTO> ReserveSeatAsync(ReserveSeatDTO reserveSeatDTO)
        {
            try
            {
                var isAvailable = await IsSeatAvailableAsync(reserveSeatDTO.ScheduleId, 
                    reserveSeatDTO.SeatId);

                if (!isAvailable)
                    throw new Exception("The seat is already reserved!");

                // create a new ticket for the reservation
                var ticketForDb = _mapper.Map<Ticket>(reserveSeatDTO);
                
                // adding ticket to db and save database
                _unitOfWork.Ticket.Add(ticketForDb);

                await _unitOfWork.Save();

                return _mapper.Map<SeatReservationDTO>(ticketForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CancelSeatReservationAsync(Guid reservationId)
        {
            try
            {
                var ticketFromDb = _unitOfWork.Ticket.Get(t => t.TicketId.Equals(reservationId))
                     ?? throw new Exception("Reservation not found!");


                // removing ticket from db and save database
                _unitOfWork.Ticket.Remove(ticketFromDb);

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
