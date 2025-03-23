using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AnimeFranchises.Infrastructure.Data;

public class AnimeFranchiseDbContext(DbContextOptions<AnimeFranchiseDbContext> options) : DbContext(options)
{
    public DbSet<AnimeFranchise> AnimeFranchises { get; set; }
    public DbSet<AnimeFranchiseInfo> AnimeFranchiseInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new AnimeFranchiseConfiguration())
            .ApplyConfiguration(new AnimeFranchiseInfoConfiguration());
    }
}