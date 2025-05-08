namespace AnimeService.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAnimeRepository AnimeRepository { get; }
    IAnimeTranslationRepository AnimeTranslationRepository { get; }
    
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}