using AnimeService.Domain.Entities;

namespace AnimeService.Domain.Interfaces;

public interface IAnimeSerialRepository
{
    Task<AnimeSerial> CreateAsync(AnimeSerial animeSerial);
    Task<IEnumerable<AnimeSerial>> GetAllAsync();
    Task<AnimeSerial?> GetByIdAsync(Guid id);
    Task<AnimeSerial?> UpdateAsync(Guid id, AnimeSerial animeSerial);
    Task<AnimeSerial?> DeleteAsync(Guid id);
}