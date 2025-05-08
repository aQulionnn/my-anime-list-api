using AnimeService.Domain.Entities;

namespace AnimeService.Domain.Interfaces;

public interface IAnimeRepository
{
    Task<Anime> CreateAsync(Anime anime);
    Task<IEnumerable<Anime>> GetAllAsync();
    Task<Anime?> GetByIdAsync(Guid id);
    Task<Anime?> UpdateAsync(Guid id, Anime anime);
    Task<Anime?> DeleteAsync(Guid id);
}