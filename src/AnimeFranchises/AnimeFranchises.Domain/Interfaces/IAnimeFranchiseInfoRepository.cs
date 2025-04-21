using AnimeFranchises.Domain.Entities;

namespace AnimeFranchises.Domain.Interfaces;

public interface IAnimeFranchiseInfoRepository
{
    Task<FranchiseTranslation> CreateAsync(FranchiseTranslation franchiseTranslation);
    Task<IEnumerable<FranchiseTranslation>> GetAllAsync();
    Task<FranchiseTranslation?> GetByIdAsync(Guid id);
    Task<FranchiseTranslation?> UpdateAsync(Guid id, FranchiseTranslation franchiseTranslation);
    Task<FranchiseTranslation?> DeleteAsync(Guid id);
}