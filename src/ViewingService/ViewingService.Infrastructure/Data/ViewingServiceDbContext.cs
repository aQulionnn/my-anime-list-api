using Microsoft.EntityFrameworkCore;
using ViewingService.Domain.Entities;
using ViewingService.Infrastructure.Data.Configurations;

namespace ViewingService.Infrastructure.Data;

public class ViewingServiceDbContext(DbContextOptions<ViewingServiceDbContext> options) : DbContext(options)
{
    public DbSet<ViewingInfo> ViewingInfos { get; set; }
    public DbSet<ViewingStat> ViewingStats { get; set; }
    public DbSet<AnimeReference> AnimeReferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new ViewingInfoConfiguration())
            .ApplyConfiguration(new ViewingStatConfiguration());
    }
}