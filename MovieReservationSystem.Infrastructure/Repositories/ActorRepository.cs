using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class ActorRepository(AppDbContext db) : Repository<Actor>(db), IActorRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Actor actor)
        {
            _db.Actors.Update(actor);
        }
    }
}
