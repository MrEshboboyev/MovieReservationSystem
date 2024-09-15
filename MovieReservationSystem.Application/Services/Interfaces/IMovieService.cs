using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDetailsDTO>> GetAllMoviesWithDetailsAsync();
        Task<MovieDetailsDTO> GetMovieByIdAsync(Guid movieId);
        Task<MovieDTO> CreateMovieAsync(CreateMovieDTO createMovieDTO);
        Task<MovieDTO> UpdateMovieAsync(Guid movieId, UpdateMovieDTO updateMovieDTO);
        Task<bool> DeleteMovieAsync(Guid movieId);

        // get movie schedules
        Task<IEnumerable<MovieScheduleDTO>> GetMovieSchedulesAsync(Guid movieId);

        // add a new schedule to a movie
        Task<MovieScheduleDTO> AddMovieScheduleAsync(Guid movieId, CreateMovieScheduleDTO createMovieScheduleDTO);

        // Get all currently showing movies
        Task<IEnumerable<MovieDTO>> GetCurrentlyShowingMoviesAsync();

        // get movies by genre
        Task<IEnumerable<MovieDTO>> GetMoviesByGenreAsync(string genre);

        // get movies by actor id
        Task<IEnumerable<MovieDTO>> GetMoviesByActorAsync(Guid actorId);

        // get movies by theater id
        Task<IEnumerable<MovieDTO>> GetMoviesByTheaterAsync(Guid theaterId);

        // Search movies by title
        Task<IEnumerable<MovieDTO>> SearchMoviesByTitleAsync(string title);
    }
}
