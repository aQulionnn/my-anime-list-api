namespace FranchiseService.Domain.Entities;

public sealed class Franchise
{
    public Guid Id { get; init; }
    public int ViewingOrder { get; set; }
    public int ViewingYear { get; set; }
    
    public IEnumerable<FranchiseTranslation> FranchiseTranslations { get; set; }
}