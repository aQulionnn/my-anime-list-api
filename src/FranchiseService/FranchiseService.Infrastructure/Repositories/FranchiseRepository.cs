using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FranchiseService.Infrastructure.Repositories;

public class FranchiseRepository(FranchiseDbContext context) : IAnimeFranchiseRepository
{
    private readonly FranchiseDbContext _context = context;
    
    public async Task<Franchise> CreateAsync(Franchise franchise)
    {
        await _context.AnimeFranchises.AddAsync(franchise);
        return franchise;    
    }

    public async Task<IEnumerable<Franchise>> GetAllAsync()
    {
        return await _context.AnimeFranchises.ToListAsync();
    }

    public async Task<Franchise?> GetByIdAsync(Guid id)
    {
        return await _context.AnimeFranchises.FindAsync(id);
    }

    public async Task<Franchise?> UpdateAsync(Guid id, Franchise franchise)
    {
        var existing = await _context.AnimeFranchises.FindAsync(id);
        if (existing is null) return null;
        
        franchise.Id = id;
        
        _context.Entry(existing).CurrentValues.SetValues(franchise);
        return existing;
    }

    public async Task<Franchise?> DeleteAsync(Guid id)
    {
        var existing = await _context.AnimeFranchises.FindAsync(id);
        if (existing is null) return null;

        _context.AnimeFranchises.Remove(existing);
        return existing;
    }
}