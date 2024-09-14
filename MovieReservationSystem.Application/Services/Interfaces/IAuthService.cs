using MovieReservationSystem.Application.Common.Models;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginModel loginModel);
        Task RegisterAsync(RegisterModel registerModel);
        Task<string> GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
