using AnimeService.Domain.Enums;

namespace AnimeService.Application.Dtos.AnimeSerialInfoDtos;

public class CreateAnimeTranslationDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }

    public Guid AnimeSerialId { get; set; }
}