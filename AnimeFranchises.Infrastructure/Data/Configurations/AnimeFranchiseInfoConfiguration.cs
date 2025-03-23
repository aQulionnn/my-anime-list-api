using AnimeFranchises.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeFranchises.Infrastructure.Data.Configurations;

public class AnimeFranchiseInfoConfiguration : IEntityTypeConfiguration<AnimeFranchiseInfo>
{
    public void Configure(EntityTypeBuilder<AnimeFranchiseInfo> builder)
    {
        builder.ToTable("anime_franchise_info");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Language).HasColumnName("language");
        builder.Property(x => x.AnimeFranchiseId).HasColumnName("anime_franchise_id");
    }
}