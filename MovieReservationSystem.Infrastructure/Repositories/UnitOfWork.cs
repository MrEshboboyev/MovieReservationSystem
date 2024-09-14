using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        public IActorRepository Actor { get; } = new ActorRepository(db);
        public IMovieActorRepository MovieActor { get; } = new MovieActorRepository(db);
        public IMovieRepository Movie { get; } = new MovieRepository(db);
        public IScheduleRepository Schedule { get; } = new ScheduleRepository(db);
        public ISeatRepository Seat { get; } = new SeatRepository(db);
        public ITheaterRepository Theater { get; } = new TheaterRepository(db);
        public ITicketRepository Ticket { get; } = new TicketRepository(db);

        private readonly AppDbContext _db = db;

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
