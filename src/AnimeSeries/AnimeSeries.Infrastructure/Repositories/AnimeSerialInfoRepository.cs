using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using AnimeSeries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeSeries.Infrastructure.Repositories;

public class AnimeSerialInfoRepository(AnimeSeriesDbContext context) : IAnimeSerialInfoRepository
{
    private readonly AnimeSeriesDbContext _context = context;

    public async Task<AnimeSerialInfo> CreateAsync(AnimeSerialInfo animSerialInfo)
    {
        await _context.AnimeSerialInfos.AddAsync(animSerialInfo);
        return animSerialInfo;
    }

    public async Task<IEnumerable<AnimeSerialInfo>> GetAllAsync()
    {
        return await _context.AnimeSerialInfos.ToListAsync();
    }

    public async Task<AnimeSerialInfo?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeSerialInfos.FindAsync(id);
    }

    public async Task<AnimeSerialInfo?> UpdateAsync(Guid id, AnimeSerialInfo animSerialInfo)
    {
        var existing = await _context.AnimeSerialInfos.FindAsync(id);
        if (existing is null) return null;

        animSerialInfo.Id = id;
        animSerialInfo.AnimeSerialId = existing.AnimeSerialId;
        _context.Entry(existing).CurrentValues.SetValues(animSerialInfo);
        return existing;
    }

    public async Task<AnimeSerialInfo?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeSerialInfos.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeSerialInfos.Remove(existing);
        return existing;
    }
}
