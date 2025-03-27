using AnimeSeries.Domain.Entities;
using AnimeSeries.Domain.Interfaces;
using AnimeSeries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeSeries.Infrastructure.Repositories;

public class AnimeSerialRepository(AnimeSeriesDbContext context) : IAnimeSerialRepository
{
    private readonly AnimeSeriesDbContext _context = context;

    public async Task<AnimeSerial> CreateAsync(AnimeSerial animeSerial)
    {
        await _context.AnimeSeries.AddAsync(animeSerial);
        return animeSerial;
    }

    public async Task<IEnumerable<AnimeSerial>> GetAllAsync()
    {
        return await _context.AnimeSeries.ToListAsync();
    }

    public async Task<AnimeSerial?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeSeries.FindAsync(id);
    }

    public async Task<AnimeSerial?> UpdateAsync(Guid id, AnimeSerial animeSerial)
    {
        var existing = await _context.AnimeSeries.FindAsync(id);
        if (existing is null) return null;

        animeSerial.Id = id;
        _context.Entry(existing).CurrentValues.SetValues(animeSerial);
        return existing;
    }

    public async Task<AnimeSerial?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeSeries.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeSeries.Remove(existing);
        return existing;
    }
}
