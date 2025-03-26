using AnimeSeries.Domain.Entities;

namespace AnimeSeries.Domain.Interfaces;

public interface IAnimeSerialInfoRepository
{
    Task<AnimeSerialInfo> CreateAsync(AnimeSerialInfo animeSerialInfo);
    Task<IEnumerable<AnimeSerialInfo>> GetAllAsync();
    Task<AnimeSerialInfo?> GetByIdAsync(Guid id);
    Task<AnimeSerialInfo?> UpdateAsync(Guid id, AnimeSerialInfo animeSerialInfo);
    Task<AnimeSerialInfo?> DeleteAsync(Guid id);
}