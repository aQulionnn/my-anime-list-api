namespace AnimeService.Application.Dtos.AnimeSerialInfoDtos;

public class AnimeTranslationResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }

    public Guid AnimeSerialId { get; set; }
}