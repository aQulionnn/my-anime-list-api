using FranchiseService.Domain.Entities;
using FranchiseService.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FranchiseService.Infrastructure.Data;

public class FranchiseDbContext(DbContextOptions<FranchiseDbContext> options) : DbContext(options)
{
    public DbSet<Franchise> Franchises { get; set; }
    public DbSet<FranchiseTranslation> FranchiseTranslations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new FranchiseConfiguration())
            .ApplyConfiguration(new FranchiseTranslationConfiguration());
    }
}