using Microsoft.EntityFrameworkCore;
using AnimeService.Domain.Entities;
using AnimeService.Infrastructure.Data.Configurations;

namespace AnimeService.Infrastructure.Data;

public class AnimeSeriesDbContext(DbContextOptions<AnimeSeriesDbContext> options) : DbContext(options)
{
    public DbSet<AnimeSerial> AnimeSeries { get; set; }
    public DbSet<AnimeSerialInfo> AnimeSerialInfos { get; set; }
    public DbSet<ReWatchedAnimeSerial> ReWatchedAnimeSeries { get; set; }
    public DbSet<Studio> Studios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new AnimeSerialConfiguration())
            .ApplyConfiguration(new AnimeSerialInfoConfiguration())
            .ApplyConfiguration(new ReWatchedAnimeSerialConfiguration());
    }
}