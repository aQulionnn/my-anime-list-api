namespace AnimeService.Domain.Entities;

public sealed class Studio
{
    public Guid Id { get; init; }
    public string Name { get; set; }

    public IEnumerable<Anime> AnimeSerials { get; set; }
}