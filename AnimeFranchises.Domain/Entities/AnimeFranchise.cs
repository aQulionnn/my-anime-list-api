namespace AnimeFranchises.Domain.Entities;

public class AnimeFranchise
{
    public Guid Id { get; set; }
    public int ViewingOrder { get; set; }
    
    public IEnumerable<AnimeFranchiseInfo> AnimeFranchiseInfos { get; set; }
}