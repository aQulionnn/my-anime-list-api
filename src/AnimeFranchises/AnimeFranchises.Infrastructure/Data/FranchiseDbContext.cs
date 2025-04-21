using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AnimeFranchises.Infrastructure.Data;

public class FranchiseDbContext(DbContextOptions<FranchiseDbContext> options) : DbContext(options)
{
    public DbSet<Franchise> AnimeFranchises { get; set; }
    public DbSet<FranchiseTranslation> AnimeFranchiseInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new FranchiseConfiguration())
            .ApplyConfiguration(new FranchiseTranslationConfiguration());
    }
}