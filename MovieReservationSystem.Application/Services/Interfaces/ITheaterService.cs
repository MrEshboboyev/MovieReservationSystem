using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface ITheaterService
    {
        Task<IEnumerable<TheaterDTO>> GetAllTheatersAsync();
        Task<TheaterDTO> GetTheaterByIdAsync(Guid theaterId);
        Task<TheaterDTO> CreateTheaterAsync(CreateTheaterDTO createTheaterDTO);
        Task<TheaterDTO> UpdateTheaterAsync(Guid theaterId, UpdateTheaterDTO updateTheaterDTO);
        Task<bool> DeleteTheaterAsync(Guid theaterId);

        // Seat Management
        Task<SeatDTO> AddSeatToTheaterAsync(Guid theaterId, CreateSeatDTO createSeatDTO);
        Task<SeatDTO> UpdateSeatToTheaterAsync(Guid theaterId, Guid seatId, UpdateSeatDTO updateSeatDTO);
        Task<bool> RemoveSeatFromTheaterAsync(Guid theaterId, Guid seatId);
        Task<IEnumerable<SeatDTO>> GetSeatsByTheaterIdAsync(Guid theaterId);

        // Additional functionalities
        Task<IEnumerable<TheaterMovieScheduleDTO>> GetSchedulesByTheaterAsync(Guid theaterId);
    }
}
