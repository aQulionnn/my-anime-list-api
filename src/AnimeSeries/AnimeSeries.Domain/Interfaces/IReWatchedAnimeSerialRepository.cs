using AnimeSeries.Domain.Entities;

namespace AnimeSeries.Domain.Interfaces;

public interface IReWatchedAnimeSerialRepository
{
    Task<ReWatchedAnimeSerial> CreateAsync(ReWatchedAnimeSerial reWatchedAnimeSerial);
    Task<IEnumerable<ReWatchedAnimeSerial>> GetAllAsync();
    Task<ReWatchedAnimeSerial?> GetByIdAsync(Guid id);
    Task<ReWatchedAnimeSerial?> UpdateAsync(Guid id, ReWatchedAnimeSerial reWatchedAnimeSerial);
    Task<ReWatchedAnimeSerial?> DeleteAsync(Guid id);
}