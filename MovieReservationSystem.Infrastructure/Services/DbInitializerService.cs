using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Application.Common.Interfaces;

namespace MovieReservationSystem.Infrastructure.Services
{
    public static class DbInitializerService
    {
        public static void SeedDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.Initialize();
            }
        }
    }
}
