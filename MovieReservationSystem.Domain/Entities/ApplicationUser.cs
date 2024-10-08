﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        // relations
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
