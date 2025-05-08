namespace AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;

public class ReWatchedAnimeSerialResponseDto
{
    public Guid Id { get; set; }
    public int ReWatchedEpisodes { get; set; }
    public int ViewingOrder { get; set; }
    
    public Guid AnimeSerialId { get; set; }
}