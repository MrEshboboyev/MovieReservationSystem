﻿namespace MovieReservationSystem.Domain.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
