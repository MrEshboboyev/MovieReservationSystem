using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        public IActorRepository Actor { get; private set; } = new ActorRepository(db);
        public IMovieActorRepository MovieActor { get; private set; } = new MovieActorRepository(db);
        public IMovieRepository Movie { get; private set; } = new MovieRepository(db);
        public IScheduleRepository Schedule { get; private set; } = new ScheduleRepository(db);
        public ISeatRepository Seat { get; private set; } = new SeatRepository(db);
        public ITheaterRepository Theater { get; private set; } = new TheaterRepository(db);
        public ITicketRepository Ticket { get; private set; } = new TicketRepository(db);

        private readonly AppDbContext _db = db;

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
