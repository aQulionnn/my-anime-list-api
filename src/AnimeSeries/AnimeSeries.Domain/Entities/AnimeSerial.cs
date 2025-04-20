namespace AnimeSeries.Domain.Entities;

public class AnimeSerial
{
    public Guid Id { get; set; }
    public int Season { get; set; }
    public int Part { get; set; }
    public int Episodes { get; set; }
    public int WatchedEpisodes { get; set; }
    public int Fillers { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ViewingYear { get; set; }
    public int ViewingOrder { get; set; }
    public string PosterUrl { get; set; }
    public Guid FranchiseId { get; set; }
    public Guid StudioId { get; set; }
    public Studio Studio { get; set; }

    public IEnumerable<AnimeSerialInfo> AnimeSerialInfos { get; set; }
    public IEnumerable<ReWatchedAnimeSerial> ReWatchedAnimeSeries { get; set; }
}