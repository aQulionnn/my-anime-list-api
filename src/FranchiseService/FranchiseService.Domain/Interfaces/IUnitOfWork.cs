namespace FranchiseService.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IFranchiseRepository FranchiseRepository { get; }
    IFranchiseTranslationRepository FranchiseTranslationRepository { get; }
    
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();    
}