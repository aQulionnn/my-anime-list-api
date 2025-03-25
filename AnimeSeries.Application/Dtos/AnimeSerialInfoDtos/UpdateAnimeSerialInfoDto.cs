using AnimeSeries.Domain.Enums;

namespace AnimeSeries.Application.Dtos.AnimeSerialInfoDtos;

public class UpdateAnimeSerialInfoDto
{
    public string Title { get; set; }
    public LanguageType Language { get; set; }
}