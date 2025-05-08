using AnimeService.Domain.Enums;

namespace AnimeService.Application.Dtos.AnimeSerialInfoDtos;

public class UpdateAnimeTranslationDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
}