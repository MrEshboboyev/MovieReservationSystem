using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDetailedDTO>> GetAllActorsAsync();
        Task<IEnumerable<ActorDetailedDTO>> GetActorsByMovieAsync(Guid movieId);
        Task<ActorDetailedDTO> GetActorByIdAsync(Guid actorId);
        Task<ActorBasicDTO> CreateActorAsync(CreateActorDTO createActorDTO);
        Task<ActorBasicDTO> UpdateActorAsync(UpdateActorDTO updateActorDTO);
        Task<bool> DeleteActorAsync(Guid actorId);
    }
}
