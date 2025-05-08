namespace AnimeService.Application.Dtos.AnimeDtos;

public class AnimeResponseDto
{
    public Guid Id { get; set; }
    public string PosterUrl { get; set; }
    public string ReleaseFormat { get; set; }
    public int EpisodeCount { get; set; }
    public int FillerCount { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Guid FranchiseId { get; set; }
    public Guid StudioId { get; set; }
}
