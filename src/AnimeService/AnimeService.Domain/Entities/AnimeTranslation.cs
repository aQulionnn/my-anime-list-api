using AnimeService.Domain.Enums;

namespace AnimeService.Domain.Entities;

public class AnimeTranslation
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }

    public Guid AnimeId { get; set; }
    public Anime Anime { get; set; }
}