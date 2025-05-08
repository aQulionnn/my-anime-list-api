using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;

namespace AnimeService.Infrastructure.Repositories;

public class UnitOfWork(AnimeSeriesDbContext context) : IUnitOfWork
{
    private readonly AnimeSeriesDbContext _context = context;
    private IAnimeSerialRepository _animeSerialRepository;
    private IAnimeSerialInfoRepository _animeSerialInfoRepository;
    private IReWatchedAnimeSerialRepository _reWatchedAnimeSerialRepository;

    public IAnimeSerialRepository AnimeSerialRepository
    {
        get { return _animeSerialRepository = new AnimeSerialRepository(_context); }
    }

    public IAnimeSerialInfoRepository AnimeSerialInfoRepository
    {
        get { return _animeSerialInfoRepository = new AnimeSerialInfoRepository(_context); }
    }

    public IReWatchedAnimeSerialRepository ReWatchedAnimeSerialRepository
    {
        get { return _reWatchedAnimeSerialRepository = new ReWatchedAnimeSerialRepository(_context); }
    }

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