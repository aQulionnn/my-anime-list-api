namespace AnimeService.Application.Dtos.AnimeSerialDtos;

public class UpdateAnimeSerialDto
{
    public int Season { get; set; }
    public int Part { get; set; }
    public int Episodes { get; set; }
    public int WatchedEpisodes { get; set; }
    public int Fillers { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ViewingYear { get; set; }
    public int ViewingOrder { get; set; }
    public string PosterUrl { get; set; }
}