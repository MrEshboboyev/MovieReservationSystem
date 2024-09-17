using AutoMapper;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class ActorService(IUnitOfWork unitOfWork, IMapper mapper) : IActorService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


        public async Task<IEnumerable<ActorDetailedDTO>> GetAllActorsAsync()
        {
            try
            {
                return _mapper.Map<IEnumerable<ActorDetailedDTO>>(_unitOfWork.Actor.GetAll(
                    includeProperties: "MovieActors.Movie"
                    ));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ActorDetailedDTO> GetActorByIdAsync(Guid actorId)
        {
            try
            {
                return _mapper.Map<ActorDetailedDTO>(_unitOfWork.Actor.Get(
                    filter: a => a.ActorId.Equals(actorId),
                    includeProperties: "MovieActors.Movie"
                    ));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ActorDetailedDTO>> GetActorsByMovieAsync(Guid movieId)
        {
            try
            {
                // getting this movie
                var movieFromDb = _unitOfWork.Movie.Get(
                    filter: m => m.MovieId.Equals(movieId),
                    includeProperties: "MovieActors.Actor"
                    ) ?? throw new Exception("Movie not found!");

                var movieActors = movieFromDb.MovieActors.Select(ma => ma.Actor).ToList(); 
                return _mapper.Map<IEnumerable<ActorDetailedDTO>>(movieActors);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ActorBasicDTO> CreateActorAsync(CreateActorDTO createActorDTO)
        {
            try
            {
                // prepare actor for db
                var actorForDb = _mapper.Map<Actor>(createActorDTO);

                // adding actor and save database
                _unitOfWork.Actor.Add(actorForDb);
                await _unitOfWork.Save();

                return _mapper.Map<ActorBasicDTO>(actorForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ActorBasicDTO> UpdateActorAsync(UpdateActorDTO updateActorDTO)
        {
            try
            {
                // getting actor from db
                var actorFromDb = _unitOfWork.Actor.Get(a => a.ActorId.Equals(updateActorDTO.ActorId))
                    ?? throw new Exception("Actor not found!");

                // update field with copying from updateDTO
                _mapper.Map(updateActorDTO, actorFromDb);

                // update actor and save database
                _unitOfWork.Actor.Update(actorFromDb);
                await _unitOfWork.Save();

                return _mapper.Map<ActorBasicDTO>(actorFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteActorAsync(Guid actorId)
        {
            try
            {
                // getting actor from db
                var actorFromDb = _unitOfWork.Actor.Get(a => a.ActorId.Equals(actorId))
                    ?? throw new Exception("Actor not found!");

                // remove actor and save database
                _unitOfWork.Actor.Remove(actorFromDb);
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
