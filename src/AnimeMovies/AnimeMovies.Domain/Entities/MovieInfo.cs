using AnimeMovies.Domain.Enums;

namespace AnimeMovies.Domain.Entities;

public class MovieInfo
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }

    public Guid MovieId { get; init; }
    public Movie Movie { get; init; }
}