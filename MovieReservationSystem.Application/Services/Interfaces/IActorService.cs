using MovieReservationSystem.Application.DTOs;

namespace MovieReservationSystem.Application.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDetailsDTO>> GetAllActorsAsync();
        Task<IEnumerable<ActorDetailsDTO>> GetActorsByMovieAsync(Guid movieId);
        Task<ActorDetailsDTO> GetActorByIdAsync(Guid actorId);
        Task<ActorBasicDTO> CreateActorAsync(CreateActorDTO createActorDTO);
        Task<ActorBasicDTO> UpdateActorAsync(UpdateActorDTO updateActorDTO);
        Task<bool> DeleteActorAsync(Guid actorId);
    }
}
