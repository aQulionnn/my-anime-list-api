using AnimeFranchises.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeFranchises.Infrastructure.Data.Configurations;

public class AnimeFranchiseConfiguration : IEntityTypeConfiguration<AnimeFranchise>
{
    public void Configure(EntityTypeBuilder<AnimeFranchise> builder)
    {
        builder.ToTable("anime_franchise");

        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ViewingOrder).HasColumnName("viewing_order");
    }
}