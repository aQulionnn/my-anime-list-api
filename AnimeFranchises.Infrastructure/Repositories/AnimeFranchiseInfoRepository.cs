using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeFranchises.Infrastructure.Repositories;

public class AnimeFranchiseInfoRepository(AnimeFranchiseDbContext context) : IAnimeFranchiseInfoRepository
{
    private readonly AnimeFranchiseDbContext _context = context;
    
    public async Task<AnimeFranchiseInfo> CreateAsync(AnimeFranchiseInfo animeFranchiseInfo)
    {
        await _context.AnimeFranchiseInfos.AddAsync(animeFranchiseInfo);
        return animeFranchiseInfo;
    }

    public async Task<IEnumerable<AnimeFranchiseInfo>> GetAllAsync()
    {
        return await _context.AnimeFranchiseInfos.ToListAsync();
    }

    public async Task<AnimeFranchiseInfo?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeFranchiseInfos.FindAsync(id);
    }

    public async Task<AnimeFranchiseInfo?> UpdateAsync(Guid id, AnimeFranchiseInfo animeFranchiseInfo)
    {
        var existing = await _context.AnimeFranchiseInfos.FindAsync(id);
        if (existing is null) return null;

        animeFranchiseInfo.Id = id;
        animeFranchiseInfo.AnimeFranchiseId = existing.AnimeFranchiseId;
        
        _context.Entry(existing).CurrentValues.SetValues(animeFranchiseInfo);
        return existing;
    }

    public async Task<AnimeFranchiseInfo?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeFranchiseInfos.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeFranchiseInfos.Remove(existing);
        return existing;
    }
}