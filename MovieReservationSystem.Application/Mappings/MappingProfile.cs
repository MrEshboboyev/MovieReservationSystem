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
            CreateMap<Actor, ActorDTO>();

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
            CreateMap<Seat, SeatDTO>();

            // CreateScheduleDTO -> Schedule
            CreateMap<CreateScheduleDTO, Schedule>();
        }
    }
}
