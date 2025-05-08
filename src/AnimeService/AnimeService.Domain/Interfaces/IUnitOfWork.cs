namespace AnimeService.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAnimeSerialRepository AnimeSerialRepository { get; }
    IAnimeSerialInfoRepository AnimeSerialInfoRepository { get; }
    IReWatchedAnimeSerialRepository ReWatchedAnimeSerialRepository { get; }
    
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}