using FranchiseService.Domain.Interfaces;
using FranchiseService.Infrastructure.Data;

namespace FranchiseService.Infrastructure.Repositories;

public class UnitOfWork(FranchiseDbContext context) : IUnitOfWork
{
    private readonly FranchiseDbContext _context = context;
    private IAnimeFranchiseRepository _animeFranchiseRepo;
    private IAnimeFranchiseInfoRepository _animeFranchiseInfoRepo;
    
    public IAnimeFranchiseRepository AnimeFranchiseRepository { get { return _animeFranchiseRepo = new FranchiseRepository(_context); } }
    public IAnimeFranchiseInfoRepository AnimeFranchiseInfoRepository { get { return _animeFranchiseInfoRepo = new FranchiseTranslationRepository(_context); } }
    
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