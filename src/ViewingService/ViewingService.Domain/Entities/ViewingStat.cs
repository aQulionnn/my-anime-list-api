namespace ViewingService.Domain.Entities;

public class ViewingStat
{
    public Guid Id { get; set; }
    public int WatchedEpisodes { get; set; }
    public int Year { get; set; }
    public Guid ViewingInfoId { get; init; }
    public ViewingInfo ViewingInfo { get; init; } 
}
