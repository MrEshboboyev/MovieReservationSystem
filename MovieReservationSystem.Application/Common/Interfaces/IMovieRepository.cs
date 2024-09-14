using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie movie);
    }
}
