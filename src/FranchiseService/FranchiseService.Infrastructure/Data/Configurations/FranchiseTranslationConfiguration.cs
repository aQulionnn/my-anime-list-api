using FranchiseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FranchiseService.Infrastructure.Data.Configurations;

public class FranchiseTranslationConfiguration : IEntityTypeConfiguration<FranchiseTranslation>
{
    public void Configure(EntityTypeBuilder<FranchiseTranslation> builder)
    {
        builder.ToTable("franchise_translation");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Language).HasColumnName("language");
        builder.Property(x => x.FranchiseId).HasColumnName("franchise_id");
    }
}