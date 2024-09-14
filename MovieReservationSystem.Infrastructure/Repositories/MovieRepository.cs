using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class MovieRepository(AppDbContext db) : Repository<Movie>(db), IMovieRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Movie movie)
        {
            _db.Movies.Update(movie);
        }
    }
}
