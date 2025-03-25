namespace AnimeSeries.Domain.Entities;

public class AnimeSeries
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

    public IEnumerable<AnimeSeriesInfo> AnimeSeriesInfos { get; set; }
    public IEnumerable<AnimeSeriesReWatched> AnimeSeriesReWatcheds { get; set; }
}