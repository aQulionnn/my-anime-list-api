using AnimeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeService.Infrastructure.Data.Configurations;

public class FranchiseReferenceConfiguration : IEntityTypeConfiguration<FranchiseReference>
{
    public void Configure(EntityTypeBuilder<FranchiseReference> builder)
    {
        builder.ToTable("franchise_reference");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.FranchiseId).HasColumnName("franchise_id");
        
        builder.HasIndex(x => x.FranchiseId);
    }
}