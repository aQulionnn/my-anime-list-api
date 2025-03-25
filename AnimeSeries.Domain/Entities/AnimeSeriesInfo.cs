using AnimeSeries.Domain.Enums;

namespace AnimeSeries.Domain.Entities;

public class AnimeSeriesInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }

    public Guid AnimeSeriesId { get; set; }
    public AnimeSeries AnimeSeries { get; set; }
}