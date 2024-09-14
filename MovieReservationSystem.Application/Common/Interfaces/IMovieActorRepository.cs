using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface IMovieActorRepository : IRepository<MovieActor>
    {
        void Update(MovieActor movieActor);
    }
}
