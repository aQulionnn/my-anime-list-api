using AnimeService.Domain.Enums;

namespace AnimeService.Domain.Entities;

public class Anime
{
    public Guid Id { get; init; }
    public string PosterUrl { get; set; }
    public ReleaseFormatType ReleaseFormat { get; set; }
    public int EpisodeCount { get; set; }
    public int FillerCount { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Guid FranchiseId { get; init; }
    public Guid StudioId { get; init; }
    public Studio Studio { get; init; } 

    public IEnumerable<AnimeTranslation> AnimeTranslations { get; set; }
}