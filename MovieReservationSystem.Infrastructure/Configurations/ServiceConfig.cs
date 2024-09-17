using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Application.Settings;
using MovieReservationSystem.Infrastructure.Data;
using MovieReservationSystem.Infrastructure.Implementations;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // adding lifetime
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IPaymentService, StripePaymentService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISeatReservationService, SeatReservationService>();
            services.AddScoped<ITheaterService, TheaterService>();
            services.AddScoped<ITicketService, TicketService>();


            // Register Stripe API key configuration
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            return services;
        }
    }
}
