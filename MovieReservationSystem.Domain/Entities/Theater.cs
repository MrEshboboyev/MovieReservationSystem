using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MovieReservationSystem.Domain.Entities
{
    public class Theater
    {
        public Guid TheaterId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        // relations
        public ICollection<Seat> Seats { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
