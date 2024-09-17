using AutoMapper;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class MovieService(IUnitOfWork unitOfWork, IMapper mapper) : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<MovieDetailsDTO>> GetAllMoviesWithDetailsAsync()
        {
            try
            {
                return _mapper.Map<IEnumerable<MovieDetailsDTO>>(
                    _unitOfWork.Movie.GetAll(includeProperties: "Schedules,MovieActors.Actor"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MovieDTO>> GetCurrentlyShowingMoviesAsync()
        {
            try
            {
                // get current date
                var currentDate = DateTime.UtcNow;

                // get currently showing movies from db
                var currentlyShowingMovies = _unitOfWork.Movie.GetAll(
                    filter: movie => movie.Schedules.Any(schedule =>
                        schedule.ShowTime.Date.Equals(currentDate.Date)),
                    includeProperties: "Schedules,MovieActors",
                    tracked: false
                    );

                // mapping and return
                return _mapper.Map<IEnumerable<MovieDTO>>(currentlyShowingMovies);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovieDetailsDTO> GetMovieByIdAsync(Guid movieId)
        {
            try
            {
                return _mapper.Map<MovieDetailsDTO>(
                    _unitOfWork.Movie.GetAll(m => m.MovieId.Equals(movieId), 
                    includeProperties: "Schedules,MovieActors.Actor"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesByActorAsync(Guid actorId)
        {
            try
            {
                // get this actor movies from db
                var actorMovies = _unitOfWork.Movie.GetAll(
                    filter: movie => movie.MovieActors.Any(ma =>
                        ma.ActorId.Equals(actorId)),
                    includeProperties: "MovieActors.Actor",
                    tracked: false
                    );

                // mapping and return
                return _mapper.Map<IEnumerable<MovieDTO>>(actorMovies);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesByGenreAsync(string genre)
        {
            try
            {
                return _mapper.Map<IEnumerable<MovieDTO>>(
                    _unitOfWork.Movie.GetAll(m => m.Genre.Equals(genre)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesByTheaterAsync(Guid theaterId)
        {
            try
            {
                // get this theater movies from db
                var theaterMovies = _unitOfWork.Movie.GetAll(
                    filter: movie => movie.Schedules.Any(schedule =>
                        schedule.TheaterId.Equals(theaterId)),
                    includeProperties: "Schedules.Theater",
                    tracked: false
                    );

                // mapping and return
                return _mapper.Map<IEnumerable<MovieDTO>>(theaterMovies);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MovieScheduleDTO>> GetMovieSchedulesAsync(Guid movieId)
        {
            try
            {
                return _mapper.Map<IEnumerable<MovieScheduleDTO>>(
                    _unitOfWork.Schedule.GetAll(s => s.MovieId.Equals(movieId), 
                    includeProperties: "Theater"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MovieDTO>> SearchMoviesByTitleAsync(string title)
        {
            try
            {
                return _mapper.Map<IEnumerable<MovieDTO>>(
                    _unitOfWork.Movie.GetAll(m => m.Title.Contains(title)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovieScheduleDTO> AddMovieScheduleAsync(Guid movieId, CreateMovieScheduleDTO createMovieScheduleDTO)
        {
            try
            {
                // preparer schedule for db
                var scheduleForDb = _mapper.Map<Schedule>(createMovieScheduleDTO);

                // assign MovieId
                scheduleForDb.MovieId = movieId;

                // add schedule to db and save it
                _unitOfWork.Schedule.Add(scheduleForDb);

                await _unitOfWork.Save();

                return _mapper.Map<MovieScheduleDTO>(scheduleForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovieDTO> CreateMovieAsync(CreateMovieDTO createMovieDTO)
        {
            try
            {
                // prepare movie for db
                var movieForDb = _mapper.Map<Movie>(createMovieDTO);

                if (createMovieDTO.ActorIds != null && createMovieDTO.ActorIds.Any())
                {
                    // retrieve the actors by their ids
                    var actorsFromDb = _unitOfWork.Actor.GetAll(a => 
                        createMovieDTO.ActorIds.Contains(a.ActorId)).ToList();

                    // checking actorsFromDb.Count and receiving dto ids
                    if (actorsFromDb == null || !actorsFromDb.Count.Equals(createMovieDTO.ActorIds.Count))
                        throw new Exception("Actor(s) not found!");

                    // associate actors with this movie
                    movieForDb.MovieActors = actorsFromDb.Select(actor => new MovieActor
                    {
                        ActorId = actor.ActorId,
                        Movie = movieForDb
                    }).ToList();
                }

                // add movie to db and save db
                _unitOfWork.Movie.Add(movieForDb);

                await _unitOfWork.Save();

                // mapping and return
                return _mapper.Map<MovieDTO>(movieForDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteMovieAsync(Guid movieId)
        {
            try
            {
                // find Movie from db 
                var movieFromDb = _unitOfWork.Movie.Get(m => m.MovieId.Equals(movieId))
                    ?? throw new Exception("Movie not found!");

                // remove movie and save db
                _unitOfWork.Movie.Remove(movieFromDb);

                await _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovieDTO> UpdateMovieAsync(Guid movieId, UpdateMovieDTO updateMovieDTO)
        {
            try
            {
                // checking movie exist in db
                var movieFromDb = _unitOfWork.Movie.Get(
                    filter: m => m.MovieId.Equals(movieId),
                    includeProperties: "MovieActors",
                    tracked: true
                    ) ?? throw new Exception("Movie not found!");

                // prepare movie for db
                _mapper.Map(updateMovieDTO, movieFromDb);

                if (updateMovieDTO.ActorIds != null && updateMovieDTO.ActorIds.Any())
                {
                    // retrieve the actors by their ids
                    var actorsFromDb = _unitOfWork.Actor.GetAll(a =>
                        updateMovieDTO.ActorIds.Contains(a.ActorId)).ToList();

                    // checking actorsFromDb.Count and receiving dto ids
                    if (actorsFromDb == null || !actorsFromDb.Count.Equals(updateMovieDTO.ActorIds.Count))
                        throw new Exception("Actor(s) not found!");

                    // Current list of actor IDs already associated with the movie
                    var currentActorIds = movieFromDb.MovieActors.Select(ma => ma.ActorId).ToList();

                    // Find actors to add (those in DTO but not currently in MovieActors)
                    var actorsToAdd = actorsFromDb
                        .Where(a => !currentActorIds.Contains(a.ActorId))
                        .Select(a => new MovieActor
                        {
                            ActorId = a.ActorId,
                            MovieId = movieFromDb.MovieId
                        }).ToList();

                    // Find actors to remove (those currently in MovieActors but not in the DTO)
                    var actorsToRemove = movieFromDb.MovieActors
                        .Where(a => !updateMovieDTO.ActorIds.Contains(a.ActorId))
                        .ToList();

                    // Remove actors that are no longer associated
                    foreach (var actor in actorsToRemove)
                    {
                        movieFromDb.MovieActors.Remove(actor);
                    }

                    // Add new actors to the movie
                    foreach (var actor in actorsToAdd)
                    {
                        movieFromDb.MovieActors.Add(actor);
                    }
                }

                // update movie to db and save db
                _unitOfWork.Movie.Update(movieFromDb);

                await _unitOfWork.Save();

                // mapping and return
                return _mapper.Map<MovieDTO>(movieFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
