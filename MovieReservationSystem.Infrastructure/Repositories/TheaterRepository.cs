using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class TheaterRepository(AppDbContext db) : Repository<Theater>(db), ITheaterRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Theater theater)
        {
            _db.Theaters.Update(theater);
        }
    }
}
