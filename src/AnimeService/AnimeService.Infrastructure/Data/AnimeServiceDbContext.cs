using Microsoft.EntityFrameworkCore;
using AnimeService.Domain.Entities;
using AnimeService.Infrastructure.Data.Configurations;

namespace AnimeService.Infrastructure.Data;

public class AnimeServiceDbContext(DbContextOptions<AnimeServiceDbContext> options) : DbContext(options)
{
    public DbSet<Anime> AnimeSeries { get; set; }
    public DbSet<AnimeTranslation> AnimeSerialInfos { get; set; }
    public DbSet<Studio> Studios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new AnimeConfiguration())
            .ApplyConfiguration(new AnimeTranslationConfiguration());
    }
}