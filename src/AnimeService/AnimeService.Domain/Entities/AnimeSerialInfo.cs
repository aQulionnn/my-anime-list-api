using AnimeService.Domain.Enums;

namespace AnimeService.Domain.Entities;

public class AnimeSerialInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LanguageType Language { get; set; }

    public Guid AnimeSerialId { get; set; }
    public AnimeSerial AnimeSerial { get; set; }
}