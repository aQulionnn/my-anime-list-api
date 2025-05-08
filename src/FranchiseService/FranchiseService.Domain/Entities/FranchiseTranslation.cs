using FranchiseService.Domain.Enums;

namespace FranchiseService.Domain.Entities;

public class FranchiseTranslation
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }
    
    public Guid FranchiseId { get; set; }
    public Franchise Franchise { get; set; }
}