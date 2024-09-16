using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Many-to-Many between Movie and Actor
            builder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            builder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            builder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            // One-to-Many between Theater and Seat
            builder.Entity<Seat>()
                .HasOne(s => s.Theater)
                .WithMany(t => t.Seats)
                .HasForeignKey(s => s.TheaterId);

            // One-to-Many between Theater and Schedule
            builder.Entity<Schedule>()
                .HasOne(s => s.Theater)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TheaterId);

            // One-to-Many between Movie and Schedule
            builder.Entity<Schedule>()
                .HasOne(s => s.Movie)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.MovieId);

            // One-to-Many between Schedule and Ticket
            builder.Entity<Ticket>()
                .HasOne(s => s.Schedule)
                .WithMany(t => t.Tickets)
                .HasForeignKey(s => s.ScheduleId);

            // One-to-Many between Seat and Ticket
            builder.Entity<Ticket>()
                .HasOne(s => s.Seat)
                .WithMany(t => t.Tickets)
                .HasForeignKey(s => s.SeatId);

            // One-to-Many between ApplicationUser and Ticket
            builder.Entity<Ticket>()
                .HasOne(s => s.User)
                .WithMany(t => t.Tickets)
                .HasForeignKey(s => s.UserId);

            // Many-to-One between Payment and User
            builder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-One between Payment and Ticket
            builder.Entity<Payment>()
                .HasOne(p => p.Ticket)
                .WithOne(t => t.Payment)
                .HasForeignKey<Payment>(p => p.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
