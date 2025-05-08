using FranchiseService.Domain.Entities;

namespace FranchiseService.Domain.Interfaces;

public interface IAnimeFranchiseRepository
{
    Task<Franchise> CreateAsync(Franchise franchise);
    Task<IEnumerable<Franchise>> GetAllAsync();
    Task<Franchise?> GetByIdAsync(Guid id);
    Task<Franchise?> UpdateAsync(Guid id, Franchise franchise);
    Task<Franchise?> DeleteAsync(Guid id);
}