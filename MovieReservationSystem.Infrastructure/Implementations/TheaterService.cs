using AutoMapper;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class TheaterService(IUnitOfWork unitOfWork, IMapper mapper) : ITheaterService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TheaterDTO>> GetAllTheatersAsync()
        {
            try
            {
                return _mapper.Map<IEnumerable<TheaterDTO>>(_unitOfWork.Theater.GetAll(
                    includeProperties: "Seats"
                    ));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TheaterMovieScheduleDTO>> GetSchedulesByTheaterAsync(Guid theaterId)
        {
            try
            {
                return _mapper.Map<IEnumerable<TheaterMovieScheduleDTO>>
                    (_unitOfWork.Schedule.GetAll(
                        filter: s => s.TheaterId.Equals(theaterId),
                        includeProperties: "Movie"
                    ));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SeatDTO>> GetSeatsByTheaterIdAsync(Guid theaterId)
        {
            try
            {
                return _mapper.Map<IEnumerable<SeatDTO>>
                    (_unitOfWork.Seat.GetAll(s => s.TheaterId.Equals(theaterId)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TheaterDTO> GetTheaterByIdAsync(Guid theaterId)
        {
            try
            {
                return _mapper.Map<TheaterDTO>
                    (_unitOfWork.Theater.GetAll(
                        filter: s => s.TheaterId.Equals(theaterId),
                        includeProperties: "Seats"
                    ));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<TheaterDTO> CreateTheaterAsync(CreateTheaterDTO createTheaterDTO)
        {
            try
            {
                // prepare theater (and seats) for db
                var theaterForDb = _mapper.Map<Theater>(createTheaterDTO);

                // add theater to db and save db
                _unitOfWork.Theater.Add(theaterForDb);
                await _unitOfWork.Save();
                
                return _mapper.Map<TheaterDTO>(theaterForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TheaterDTO> UpdateTheaterAsync(Guid theaterId, UpdateTheaterDTO updateTheaterDTO)
        {
            try
            {
                // get theater from db
                var theaterFromDb = _unitOfWork.Theater.Get(t => t.TheaterId.Equals(theaterId))
                    ?? throw new Exception("Theater not found!");

                // mapping this theater and update
                _mapper.Map(updateTheaterDTO, theaterFromDb);

                _unitOfWork.Theater.Update(theaterFromDb);
                await _unitOfWork.Save();

                return _mapper.Map<TheaterDTO>(theaterFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTheaterAsync(Guid theaterId)
        {
            try
            {
                // get theater from db
                var theaterFromDb = _unitOfWork.Theater.Get(t => t.TheaterId.Equals(theaterId))
                    ?? throw new Exception("Theater not found!");

                // remove this theater
                _unitOfWork.Theater.Remove(theaterFromDb);
                await _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeatDTO> AddSeatToTheaterAsync(Guid theaterId, CreateSeatDTO createSeatDTO)
        {
            try
            {
                // get this theater
                var theaterFromDb = _unitOfWork.Theater.Get(
                    filter: t => t.TheaterId.Equals(theaterId),
                    includeProperties: "Seats")
                    ?? throw new Exception("Theater not found!");

                // prepare Seat for Db (mapping and assign theaterId)
                var seatForDb = _mapper.Map<Seat>(createSeatDTO);

                // adding to DB
                theaterFromDb.Seats.Add(seatForDb);

                // update and save db
                _unitOfWork.Theater.Update(theaterFromDb);
                await _unitOfWork.Save();

                return _mapper.Map<SeatDTO>(seatForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeatDTO> UpdateSeatToTheaterAsync(Guid theaterId, Guid seatId, UpdateSeatDTO updateSeatDTO)
        {
            try
            {
                // get this seat
                var seatFromDb = _unitOfWork.Seat.Get(
                    filter: s => s.TheaterId.Equals(theaterId) && 
                    s.SeatId.Equals(seatId))
                    ?? throw new Exception("Seat not found!");

                // prepare Seat for Db (mapping )
                _mapper.Map(updateSeatDTO, seatFromDb);

                // update and save db
                _unitOfWork.Seat.Update(seatFromDb);
                await _unitOfWork.Save();

                return _mapper.Map<SeatDTO>(seatFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveSeatFromTheaterAsync(Guid theaterId, Guid seatId)
        {
            try
            {
                // get this seat
                var seatFromDb = _unitOfWork.Seat.Get(
                    filter: s => s.TheaterId.Equals(theaterId) &&
                    s.SeatId.Equals(seatId))
                    ?? throw new Exception("Seat not found!");

                // remove and save db
                _unitOfWork.Seat.Remove(seatFromDb);
                await _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
