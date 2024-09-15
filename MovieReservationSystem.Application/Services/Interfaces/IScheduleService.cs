using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetSchedulesForMovieAsync(Guid movieId);
        Task<IEnumerable<ScheduleDTO>> GetSchedulesForTheaterAsync(Guid theaterId);
        Task<IEnumerable<ScheduleDTO>> GetSchedulesInRangeAsync(DateTime starDate, DateTime endDate);
        Task<IEnumerable<ScheduleDTO>> GetAvailableSchedulesAsync(ScheduleFilterDTO scheduleFilterDTO);
        Task<ScheduleDetailsDTO> GetScheduleByIdAsync(Guid scheduleId);
        Task<ScheduleDTO> CreateScheduleAsync(CreateScheduleDTO createScheduleDTO);
        Task<ScheduleDTO> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleDTO updateScheduleDTO);
        Task DeleteScheduleAsync(Guid scheduleId);
    }
}
