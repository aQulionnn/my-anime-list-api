using AnimeFranchises.Domain.Enums;

namespace AnimeFranchises.Domain.Entities;

public class AnimeFranchiseInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid AnimeFranchiseId { get; set; }
    public AnimeFranchise AnimeFranchise { get; set; }
}