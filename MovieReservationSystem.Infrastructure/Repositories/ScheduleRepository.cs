using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class ScheduleRepository(AppDbContext db) : Repository<Schedule>(db), IScheduleRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Schedule schedule)
        {
            _db.Schedules.Update(schedule);
        }
    }
}
