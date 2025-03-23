using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeFranchises.Infrastructure.Repositories;

public class AnimeFranchiseRepository(AnimeFranchiseDbContext context) : IAnimeFranchiseRepository
{
    private readonly AnimeFranchiseDbContext _context = context;
    
    public async Task<AnimeFranchise> CreateAsync(AnimeFranchise animeFranchise)
    {
        await _context.AnimeFranchises.AddAsync(animeFranchise);
        return animeFranchise;    
    }

    public async Task<IEnumerable<AnimeFranchise>> GetAllAsync()
    {
        return await _context.AnimeFranchises.ToListAsync();
    }

    public async Task<AnimeFranchise?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeFranchises.FindAsync(id);
    }

    public async Task<AnimeFranchise?> UpdateAsync(Guid id, AnimeFranchise animeFranchise)
    {
        var existing = await _context.AnimeFranchises.FindAsync(id);
        if (existing is null) return null;
        
        _context.Entry(existing).CurrentValues.SetValues(animeFranchise);
        return existing;
    }

    public async Task<AnimeFranchise?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeFranchises.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeFranchises.Remove(existing);
        return existing;
    }
}