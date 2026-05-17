using AnimeTracker.Application.DTOs.Anime;

namespace AnimeTracker.Application.Interfaces.Services
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeDto>> GetAllAnimesAsync();
        Task<AnimeDto?> GetAnimeByIdAsync(Guid id);
        Task<AnimeDto> CreateAnimeAsync(AnimeCreateDto createDto);
    }
}