using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class TicketRepository(AppDbContext db) : Repository<Ticket>(db), ITicketRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
        }
    }
}
