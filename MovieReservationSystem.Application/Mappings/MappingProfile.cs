using AutoMapper;
using MovieReservationSystem.Application.DTOs;
using MovieReservationSystem.Domain.Entities;

namespace MovieReservationSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Movie -> MovieDTO
            CreateMap<Movie, MovieDTO>();

            // Movie -> MovieDetailsDTO (with actors and schedules)
            CreateMap<Movie, MovieDetailsDTO>()
                .ForMember(dest => dest.Actors,
                opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Actor)))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules));

            // Actor -> ActorDTO
            CreateMap<Actor, ActorBasicDTO>();

            // Schedule -> MovieScheduleDTO
            CreateMap<Schedule, MovieScheduleDTO>()
                .ForMember(dest => dest.TheaterName, opt => opt.MapFrom(src => src.Theater.Name));

            // CreateMovieDTO -> Movie
            CreateMap<CreateMovieDTO, Movie>();

            // UpdateMovieDTO -> Movie
            CreateMap<UpdateMovieDTO, Movie>();

            // CreateMovieScheduleDTO -> Schedule
            CreateMap<CreateMovieScheduleDTO, Schedule>();

            // Schedule -> ScheduleDetailsDTO
            CreateMap<Schedule, ScheduleDetailsDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.TheaterName, opt => opt.MapFrom(src => src.Theater.Name));

            // Seat -> SeatDTO
            CreateMap<Seat, SeatDTO>()
                .ForMember(dest => dest.IsAvailable, opt => opt.Ignore());

            // CreateScheduleDTO -> Schedule
            CreateMap<CreateScheduleDTO, Schedule>();

            // Ticket -> SeatReservationDTO
            CreateMap<Ticket, SeatReservationDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Schedule.Movie.Title))
                .ForMember(dest => dest.TheaterName, opt => opt.MapFrom(src => src.Schedule.Theater.Name))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ShowTime, opt => opt.MapFrom(src => src.Schedule.ShowTime));

            // ReserveSeatDTO -> Ticket
            CreateMap<ReserveSeatDTO, Ticket>()
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Schedule -> TheaterMovieScheduleDTO
            CreateMap<Schedule, TheaterMovieScheduleDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));

            // CreateTheaterDTO -> Theater
            CreateMap<CreateTheaterDTO, Theater>();

            // CreateSeatDTO -> Seat
            CreateMap<CreateSeatDTO, Seat>();

            // UpdateTheaterDTO -> Theater
            CreateMap<UpdateTheaterDTO, Theater>();

            // UpdateSeatDTO -> Seat
            CreateMap<UpdateSeatDTO, Seat>();

            // Theater -> TheaterDTO 
            CreateMap<Theater, TheaterDTO>();

            // Payment -> PaymentDTO
            CreateMap<Payment, PaymentDTO>();

            // Ticket -> TicketDTO
            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Schedule.Movie.Title))
                .ForMember(dest => dest.ShowTime, opt => opt.MapFrom(src => src.Schedule.ShowTime))
                .ForMember(dest => dest.TheaterName, opt => opt.MapFrom(src => src.Schedule.Theater.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Payment.Status));

            // TicketPurchaseRequestDTO -> Ticket 
            CreateMap<TicketPurchaseRequestDTO, Ticket>()
                .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.ScheduleId))
                // Seat mapping logic will be handled in the service or repository layer, not directly in AutoMapper
                .ForMember(dest => dest.SeatId, opt => opt.Ignore())  // Seat mapping will happen in the service
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.UtcNow))  // Set purchase date to current date
                .ForMember(dest => dest.Price, opt => opt.Ignore())  // Price would be set later (could come from schedule, for example)
                .ForMember(dest => dest.UserId, opt => opt.Ignore())  // This will be set from the logged-in user
                .ForMember(dest => dest.Payment, opt => opt.Ignore());  // Payment will be set after Stripe payment confirmation
        }
    }
}
