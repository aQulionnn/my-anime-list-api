using AnimeFranchises.Domain.Entities;

namespace AnimeFranchises.Domain.Interfaces;

public interface IAnimeFranchiseInfoRepository
{
    Task<AnimeFranchiseInfo> CreateAsync(AnimeFranchiseInfo animeFranchiseInfo);
    Task<IEnumerable<AnimeFranchiseInfo>> GetAllAsync();
    Task<AnimeFranchiseInfo?> GetByIdAsync(Guid id);
    Task<AnimeFranchiseInfo?> UpdateAsync(Guid id, AnimeFranchiseInfo animeFranchiseInfo);
    Task<AnimeFranchiseInfo?> DeleteAsync(Guid id);
}