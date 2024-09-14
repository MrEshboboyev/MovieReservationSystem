using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
