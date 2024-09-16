namespace MovieReservationSystem.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IActorRepository Actor { get; }
        IMovieActorRepository MovieActor { get; }
        IMovieRepository Movie { get; }
        IPaymentRepository Payment { get; }
        IScheduleRepository Schedule { get; }
        ISeatRepository Seat { get; }
        ITheaterRepository Theater { get; }
        ITicketRepository Ticket { get; }

        Task Save();
    }
}
