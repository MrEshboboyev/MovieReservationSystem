using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface ISeatReservationService
    {
        Task<SeatReservationDTO> ReserveSeatAsync(ReserveSeatDTO reserveSeatDTO);
        Task<bool> IsSeatAvailableAsync(Guid scheduleId, Guid seatId);
        Task<IEnumerable<SeatDTO>> GetAvailableSeatsAsync(Guid scheduleId);
        Task CancelSeatReservationAsync(Guid reservationId);
        Task<IEnumerable<SeatReservationDTO>> GetUserReservationsAsync(string userId);
        Task<IEnumerable<SeatReservationDTO>> GetReservationsForScheduleAsync(Guid scheduleId);
    }
}
