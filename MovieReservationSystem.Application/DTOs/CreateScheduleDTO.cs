﻿namespace MovieReservationSystem.Application.DTOs
{
    public class CreateScheduleDTO
    {
        public Guid MovieId { get; set; }
        public DateTime ShowTime { get; set; }
        public int TheaterId { get; set; }
    }
}
