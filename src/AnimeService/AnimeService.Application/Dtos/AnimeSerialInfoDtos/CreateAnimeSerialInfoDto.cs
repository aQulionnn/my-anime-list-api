using AnimeService.Domain.Enums;

namespace AnimeService.Application.Dtos.AnimeSerialInfoDtos;

public class CreateAnimeSerialInfoDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }

    public Guid AnimeSerialId { get; set; }
}