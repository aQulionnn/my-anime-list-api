using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using AnimeSeries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeSeries.Infrastructure.Repositories;

public class ReWatchedAnimeSerialRepository(AnimeSeriesDbContext context) : IReWatchedAnimeSerialRepository
{
    private readonly AnimeSeriesDbContext _context = context;

    public async Task<ReWatchedAnimeSerial> CreateAsync(ReWatchedAnimeSerial reWatchedAnimeSerial)
    {
        await _context.ReWatchedAnimeSeries.AddAsync(reWatchedAnimeSerial);
        return reWatchedAnimeSerial;
    }

    public async Task<IEnumerable<ReWatchedAnimeSerial>> GetAllAsync()
    {
        return await _context.ReWatchedAnimeSeries.ToListAsync();
    }

    public async Task<ReWatchedAnimeSerial?> GetByIdAsync(Guid id)
    {
        return await _context.ReWatchedAnimeSeries.FindAsync(id);
    }

    public async Task<ReWatchedAnimeSerial?> UpdateAsync(Guid id, ReWatchedAnimeSerial reWatchedAnimeSerial)
    {
        var existing = await _context.ReWatchedAnimeSeries.FindAsync(id);
        if (existing is null) return null;

        reWatchedAnimeSerial.Id = id;
        reWatchedAnimeSerial.AnimeSerialId = existing.AnimeSerialId;
        _context.Entry(existing).CurrentValues.SetValues(reWatchedAnimeSerial);
        return existing;
    }

    public async Task<ReWatchedAnimeSerial?> DeleteAsync(Guid id)
    {
        var existing = await _context.ReWatchedAnimeSeries.FindAsync(id);
        if (existing is null) return null;

        _context.ReWatchedAnimeSeries.Remove(existing);
        return existing;
    }
}
