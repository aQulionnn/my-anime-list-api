namespace AnimeMovies.Domain.Entities;

public class ReWatchedMovie
{
    public Guid Id { get; init; }
    public int  ViewingOrder { get; set; }
    
    public Guid MovieId { get; init; }
    public Movie Movie { get; init; }
}