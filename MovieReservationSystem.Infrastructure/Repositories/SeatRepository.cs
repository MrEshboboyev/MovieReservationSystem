using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class SeatRepository(AppDbContext db) : Repository<Seat>(db), ISeatRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Seat seat)
        {
            _db.Seats.Update(seat);
        }
    }
}
