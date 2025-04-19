namespace AnimeMovies.Domain.Entities;

public class Movie
{
    public Guid Id { get; init; }
    public TimeSpan Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ViewingYear { get; set; }
    public int ViewingOrder { get; set; }
    public string PosterUrl { get; set; }
    
    public Guid FranchiseId { get; init; }
}