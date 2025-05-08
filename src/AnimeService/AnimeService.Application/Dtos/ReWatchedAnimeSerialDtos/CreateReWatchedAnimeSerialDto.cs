namespace AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;

public class CreateReWatchedAnimeSerialDto
{
    public int ReWatchedEpisodes { get; set; }
    public int ViewingOrder { get; set; }
    
    public Guid AnimeSerialId { get; set; }
}