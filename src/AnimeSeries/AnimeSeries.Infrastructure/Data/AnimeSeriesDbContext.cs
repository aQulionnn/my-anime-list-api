using Microsoft.EntityFrameworkCore;
using AnimeSeries.Domain.Entities;
using AnimeSeries.Infrastructure.Data.Configurations;

namespace AnimeSeries.Infrastructure.Data;

public class AnimeSeriesDbContext(DbContextOptions<AnimeSeriesDbContext> options) : DbContext(options)
{
    public DbSet<AnimeSerial> AnimeSeries { get; set; }
    public DbSet<AnimeSerialInfo> AnimeSerialInfos { get; set; }
    public DbSet<ReWatchedAnimeSerial> ReWatchedAnimeSeries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new AnimeSerialConfiguration())
            .ApplyConfiguration(new AnimeSerialInfoConfiguration())
            .ApplyConfiguration(new ReWatchedAnimeSerialConfiguration());
    }
}