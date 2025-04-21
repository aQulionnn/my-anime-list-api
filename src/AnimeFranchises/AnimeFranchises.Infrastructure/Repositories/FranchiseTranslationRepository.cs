using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeFranchises.Infrastructure.Repositories;

public class FranchiseTranslationRepository(FranchiseDbContext context) : IAnimeFranchiseInfoRepository
{
    private readonly FranchiseDbContext _context = context;
    
    public async Task<FranchiseTranslation> CreateAsync(FranchiseTranslation franchiseTranslation)
    {
        await _context.AnimeFranchiseInfos.AddAsync(franchiseTranslation);
        return franchiseTranslation;
    }

    public async Task<IEnumerable<FranchiseTranslation>> GetAllAsync()
    {
        return await _context.AnimeFranchiseInfos.ToListAsync();
    }

    public async Task<FranchiseTranslation?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeFranchiseInfos.FindAsync(id);
    }

    public async Task<FranchiseTranslation?> UpdateAsync(Guid id, FranchiseTranslation franchiseTranslation)
    {
        var existing = await _context.AnimeFranchiseInfos.FindAsync(id);
        if (existing is null) return null;

        franchiseTranslation.Id = id;
        franchiseTranslation.FranchiseId = existing.FranchiseId;
        
        _context.Entry(existing).CurrentValues.SetValues(franchiseTranslation);
        return existing;
    }

    public async Task<FranchiseTranslation?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeFranchiseInfos.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeFranchiseInfos.Remove(existing);
        return existing;
    }
}