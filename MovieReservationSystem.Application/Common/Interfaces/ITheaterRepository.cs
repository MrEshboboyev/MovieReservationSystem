using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface ITheaterRepository : IRepository<Theater>
    {
        void Update(Theater theater);
    }
}
