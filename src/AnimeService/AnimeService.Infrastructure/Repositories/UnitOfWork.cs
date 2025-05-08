using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;

namespace AnimeService.Infrastructure.Repositories;

public class UnitOfWork(AnimeServiceDbContext context) : IUnitOfWork
{
    private readonly AnimeServiceDbContext _context = context;
    private IAnimeRepository _animeRepository;
    private IAnimeTranslationRepository _animeTranslationRepository;
    
    public IAnimeRepository AnimeRepository
    {
        get { return _animeRepository = new AnimeRepository(_context); }
    }

    public IAnimeTranslationRepository AnimeTranslationRepository
    {
        get { return _animeTranslationRepository = new AnimeTranslationRepository(_context); }
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