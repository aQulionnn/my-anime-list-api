using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Infrastructure.Data;

namespace AnimeFranchises.Infrastructure.Repositories;

public class UnitOfWork(AnimeFranchiseDbContext context) : IUnitOfWork
{
    private readonly AnimeFranchiseDbContext _context = context;
    private IAnimeFranchiseRepository _animeFranchiseRepo;
    private IAnimeFranchiseInfoRepository _animeFranchiseInfoRepo;
    
    public IAnimeFranchiseRepository AnimeFranchiseRepository { get { return _animeFranchiseRepo = new AnimeFranchiseRepository(_context); } }
    public IAnimeFranchiseInfoRepository AnimeFranchiseInfoRepository { get { return _animeFranchiseInfoRepo = new AnimeFranchiseInfoRepository(_context); } }
    
    public async Task BeginAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.Database.RollbackTransactionAsync();  
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}