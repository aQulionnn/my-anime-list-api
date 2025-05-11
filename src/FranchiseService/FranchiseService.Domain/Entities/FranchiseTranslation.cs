using FranchiseService.Domain.Enums;

namespace FranchiseService.Domain.Entities;

public class FranchiseTranslation
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid FranchiseId { get; init; }
    public Franchise Franchise { get; set; }
}