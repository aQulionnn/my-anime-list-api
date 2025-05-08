using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeService.Infrastructure.Repositories;

public class AnimeRepository(AnimeServiceDbContext context) : IAnimeRepository
{
    private readonly AnimeServiceDbContext _context = context;

    public async Task<Anime> CreateAsync(Anime anime)
    {
        await _context.AnimeSeries.AddAsync(anime);
        return anime;
    }

    public async Task<IEnumerable<Anime>> GetAllAsync()
    {
        return await _context.AnimeSeries.ToListAsync();
    }

    public async Task<Anime?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeSeries.FindAsync(id);
    }

    public async Task<Anime?> UpdateAsync(Guid id, Anime anime)
    {
        var existing = await _context.AnimeSeries.FindAsync(id);
        if (existing is null) return null;
        
        _context.Entry(existing).CurrentValues.SetValues(anime);
        return existing;
    }

    public async Task<Anime?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeSeries.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeSeries.Remove(existing);
        return existing;
    }
}
