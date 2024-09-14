using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void Update(Ticket ticket);
    }
}
