using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Application.Services.Interfaces;
using MovieReservationSystem.Infrastructure.Data;
using MovieReservationSystem.Infrastructure.Implementations;

namespace MovieReservationSystem.Infrastructure.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // adding lifetime
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
