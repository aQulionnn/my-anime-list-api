using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeService.Infrastructure.Repositories;

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
