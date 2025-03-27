using AnimeFranchises.Domain.Entities;

namespace AnimeFranchises.Domain.Interfaces;

public interface IAnimeFranchiseRepository
{
    Task<AnimeFranchise> CreateAsync(AnimeFranchise animeFranchise);
    Task<IEnumerable<AnimeFranchise>> GetAllAsync();
    Task<AnimeFranchise?> GetByIdAsync(Guid id);
    Task<AnimeFranchise?> UpdateAsync(Guid id, AnimeFranchise animeFranchise);
    Task<AnimeFranchise?> DeleteAsync(Guid id);
}