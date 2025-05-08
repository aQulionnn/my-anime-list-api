using AnimeService.Domain.Entities;

namespace AnimeService.Domain.Interfaces;

public interface IAnimeTranslationRepository
{
    Task<AnimeTranslation> CreateAsync(AnimeTranslation animeTranslation);
    Task<IEnumerable<AnimeTranslation>> GetAllAsync();
    Task<AnimeTranslation?> GetByIdAsync(Guid id);
    Task<AnimeTranslation?> UpdateAsync(Guid id, AnimeTranslation animeTranslation);
    Task<AnimeTranslation?> DeleteAsync(Guid id);
}