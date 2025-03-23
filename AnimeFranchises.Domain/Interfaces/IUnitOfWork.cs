namespace AnimeFranchises.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAnimeFranchiseRepository AnimeFranchiseRepository { get; }
    IAnimeFranchiseInfoRepository AnimeFranchiseInfoRepository { get; }
    
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();    
}