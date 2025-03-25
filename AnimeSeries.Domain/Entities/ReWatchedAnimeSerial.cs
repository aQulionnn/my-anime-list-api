namespace AnimeSeries.Domain.Entities;

public class ReWatchedAnimeSerial
{
    public Guid Id { get; set; }
    public int ReWatchedEpisodes { get; set; }
    public int ViewingOrder { get; set; }
    
    public Guid AnimeSerialId { get; set; }
    public AnimeSerial AnimeSerial { get; set; }
}