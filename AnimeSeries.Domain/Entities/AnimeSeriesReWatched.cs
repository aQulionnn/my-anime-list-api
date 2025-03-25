namespace AnimeSeries.Domain.Entities;

public class AnimeSeriesReWatched
{
    public Guid Id { get; set; }
    public int ReWatchedEpisodes { get; set; }
    public int ViewingOrder { get; set; }
    
    public Guid AnimeSeriesId { get; set; }
    public AnimeSeries AnimeSeries { get; set; }
}