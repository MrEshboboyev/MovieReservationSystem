using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface IActorRepository : IRepository<Actor>
    {
        void Update(Actor actor);
    }
}
