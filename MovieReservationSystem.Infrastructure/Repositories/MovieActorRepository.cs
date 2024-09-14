using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class MovieActorRepository(AppDbContext db) : Repository<MovieActor>(db), IMovieActorRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(MovieActor movieActor)
        {
            _db.MovieActors.Update(movieActor);
        }
    }
}
