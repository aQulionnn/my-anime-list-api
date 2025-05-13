using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FranchiseService.Infrastructure.Repositories;

public class FranchiseTranslationRepository(FranchiseDbContext context) : IFranchiseTranslationRepository
{
    private readonly FranchiseDbContext _context = context;
    
    public async Task<FranchiseTranslation> CreateAsync(FranchiseTranslation franchiseTranslation)
    {
        await _context.FranchiseTranslations.AddAsync(franchiseTranslation);
        return franchiseTranslation;
    }

    public async Task<IEnumerable<FranchiseTranslation>> GetAllAsync()
    {
        return await _context.FranchiseTranslations.ToListAsync();
    }

    public async Task<FranchiseTranslation?> GetByIdAsync(Guid id)
    {
        return await _context.FranchiseTranslations.FindAsync(id);
    }

    public async Task<FranchiseTranslation?> UpdateAsync(Guid id, FranchiseTranslation franchiseTranslation)
    {
        var existing = await _context.FranchiseTranslations.FindAsync(id);
        if (existing is null) return null;
        
        _context.Entry(existing).CurrentValues.SetValues(franchiseTranslation);
        return existing;
    }

    public async Task<FranchiseTranslation?> DeleteAsync(Guid id)
    {
        var existing = await _context.FranchiseTranslations.FindAsync(id);
        if (existing is null) return null;

        _context.FranchiseTranslations.Remove(existing);
        return existing;
    }
}