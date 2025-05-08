namespace AnimeService.Domain.Entities;

public class Studio
{
    public Guid Id { get; init; }
    public string Name { get; set; }

    public IEnumerable<AnimeSerial> AnimeSerials { get; set; }
}