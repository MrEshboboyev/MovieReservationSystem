using AutoMapper;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;
using System.Linq.Expressions;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class ScheduleService(IUnitOfWork unitOfWork, IMapper mapper) : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ScheduleDTO>> GetAvailableSchedulesAsync(ScheduleFilterDTO scheduleFilterDTO)
        {
            try
            {
                // Build the filter expression based on the provided filter criteria
                Expression<Func<Schedule, bool>> filter = s =>
                    (!scheduleFilterDTO.MovieId.HasValue || s.MovieId == scheduleFilterDTO.MovieId.Value) &&
                    (!scheduleFilterDTO.TheaterId.HasValue || s.TheaterId == scheduleFilterDTO.TheaterId.Value) &&
                    (!scheduleFilterDTO.StartDate.HasValue || s.ShowTime >= scheduleFilterDTO.StartDate.Value) &&
                    (!scheduleFilterDTO.EndDate.HasValue || s.ShowTime <= scheduleFilterDTO.EndDate.Value);

                // retrieve schedules with filter
                var schedulesFromDb = _unitOfWork.Schedule.GetAll(filter, includeProperties: "Movie,Theater");

                return _mapper.Map<IEnumerable<ScheduleDTO>>(schedulesFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ScheduleDetailsDTO> GetScheduleByIdAsync(Guid scheduleId)
        {
            try
            {
                // get schedule with theater seats
                var scheduleFromDb = _unitOfWork.Schedule.Get(
                    filter: s => s.ScheduleId.Equals(scheduleId),
                    includeProperties: "Theater.Seats"
                    );

                return _mapper.Map<ScheduleDetailsDTO>(scheduleFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ScheduleDTO>> GetSchedulesForMovieAsync(Guid movieId)
        {
            try
            {
                // get schedule with movie 
                var scheduleFromDb = _unitOfWork.Schedule.GetAll(
                    filter: s => s.MovieId.Equals(movieId),
                    includeProperties: "Movie,Theater"
                    );

                return _mapper.Map<IEnumerable<ScheduleDTO>>(scheduleFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ScheduleDTO>> GetSchedulesForTheaterAsync(Guid theaterId)
        {
            try
            {
                // get schedule with theater 
                var scheduleFromDb = _unitOfWork.Schedule.GetAll(
                    filter: s => s.TheaterId.Equals(theaterId),
                    includeProperties: "Movie,Theater"
                    );

                return _mapper.Map<IEnumerable<ScheduleDTO>>(scheduleFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ScheduleDTO>> GetSchedulesInRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                // get schedule with range
                var scheduleFromDb = _unitOfWork.Schedule.GetAll(
                    filter: s => s.ShowTime >= startDate.ToUniversalTime() && s.ShowTime <= endDate.ToUniversalTime(),
                    includeProperties: "Movie,Theater"
                    );

                return _mapper.Map<IEnumerable<ScheduleDTO>>(scheduleFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ScheduleDTO> CreateScheduleAsync(CreateScheduleDTO createScheduleDTO)
        {
            try
            {
                // prepare schedule with movie and theater by ids 
                var movieExist = _unitOfWork.Movie.Any(m => m.MovieId.Equals(createScheduleDTO.MovieId));
                var theaterExist = _unitOfWork.Theater.Any(m => m.TheaterId.Equals(createScheduleDTO.TheaterId));

                if (!theaterExist || !movieExist)
                    throw new Exception("Theater/Movie not found!");

                // checking this date is not movie in this theater
                var existingSchedule = _unitOfWork.Schedule.Get(
                    s => s.TheaterId.Equals(createScheduleDTO.TheaterId) &&
                    s.ShowTime.Equals(createScheduleDTO.ShowTime.ToUniversalTime())
                    );

                if (existingSchedule != null)
                    throw new Exception("A movie is already scheduled in this theater on the same day!" +
                        $"Schedule Id : {existingSchedule.ScheduleId}");

                // prepare for db
                var scheduleForDb = _mapper.Map<Schedule>(createScheduleDTO); 
                
                // adding schedule to db and save database
                _unitOfWork.Schedule.Add(scheduleForDb);

                await _unitOfWork.Save();

                return _mapper.Map<ScheduleDTO>(scheduleForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ScheduleDTO> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleDTO updateScheduleDTO)
        {
            try
            {
                // get this schedule
                var scheduleFromDb = _unitOfWork.Schedule.Get(s =>
                    s.ScheduleId.Equals(scheduleId)) 
                    ?? throw new Exception("Schedule not found!");

                // checking showtime in this theater is available (with updating showtime)
                var existingSchedule = _unitOfWork.Schedule.Get(
                    filter: s => s.TheaterId.Equals(scheduleFromDb.TheaterId) &&
                    s.ShowTime.Equals(updateScheduleDTO.ShowTime.ToUniversalTime())
                    );

                if (existingSchedule != null)
                    throw new Exception("A movie is already scheduled in this theater on the same day!" +
                        $"Schedule Id : {existingSchedule.ScheduleId}");

                // prepare update schedule with showtime and theater id (movieId is const)
                _mapper.Map(updateScheduleDTO, scheduleFromDb);

                // adding schedule to db and save database
                _unitOfWork.Schedule.Update(scheduleFromDb);

                await _unitOfWork.Save();


                return _mapper.Map<ScheduleDTO>(scheduleFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteScheduleAsync(Guid scheduleId)
        {
            try
            {
                // get this schedule
                var scheduleFromDb = _unitOfWork.Schedule.Get(s =>
                    s.ScheduleId.Equals(scheduleId))
                    ?? throw new Exception("Schedule not found!");


                // removing schedule to db and save database
                _unitOfWork.Schedule.Remove(scheduleFromDb);

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
