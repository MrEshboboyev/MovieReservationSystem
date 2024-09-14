using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        void Update(Schedule schedule);
    }
}
