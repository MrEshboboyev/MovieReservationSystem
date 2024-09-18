using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReservationSystem.Application.Common.Models
{
    public class ReserveSeatModel
    {
        public Guid ScheduleId { get; set; }
        public Guid SeatId { get; set; }
    }
}
