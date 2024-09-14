using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface ISeatRepository : IRepository<Seat>
    {
        void Update(Seat seat);
    }
}
