namespace ViewingService.Domain.Entities;

public class ViewingInfo
{
    public Guid Id { get; init; }
    public int ViewingOrder { get; set; }
    public int ViewingYear { get; set; }
    public bool ReWatched { get; set; }
    public Guid AnimeId { get; init; }
}
