namespace AnimeFranchises.Domain.Entities;

public class Franchise
{
    public Guid Id { get; set; }
    public int ViewingOrder { get; set; }
    public int ViewingYear { get; set; }
    
    public IEnumerable<FranchiseTranslation> FranchiseTranslations { get; set; }
}