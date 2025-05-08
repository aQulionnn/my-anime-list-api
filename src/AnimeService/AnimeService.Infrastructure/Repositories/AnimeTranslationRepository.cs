using AnimeService.Domain.Entities;
using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeService.Infrastructure.Repositories;

public class AnimeTranslationRepository(AnimeServiceDbContext context) : IAnimeTranslationRepository
{
    private readonly AnimeServiceDbContext _context = context;

    public async Task<AnimeTranslation> CreateAsync(AnimeTranslation animTranslation)
    {
        await _context.AnimeSerialInfos.AddAsync(animTranslation);
        return animTranslation;
    }

    public async Task<IEnumerable<AnimeTranslation>> GetAllAsync()
    {
        return await _context.AnimeSerialInfos.ToListAsync();
    }

    public async Task<AnimeTranslation?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeSerialInfos.FindAsync(id);
    }

    public async Task<AnimeTranslation?> UpdateAsync(Guid id, AnimeTranslation animTranslation)
    {
        var existing = await _context.AnimeSerialInfos.FindAsync(id);
        if (existing is null) return null;

        animTranslation.Id = id;
        animTranslation.AnimeId = existing.AnimeId;
        _context.Entry(existing).CurrentValues.SetValues(animTranslation);
        return existing;
    }

    public async Task<AnimeTranslation?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeSerialInfos.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeSerialInfos.Remove(existing);
        return existing;
    }
}
