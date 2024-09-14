using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // adding lifetime
            services.AddScoped<IDbInitializer, DbInitializer>();
            return services;
        }
    }
}
