using FranchiseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FranchiseService.Infrastructure.Data.Configurations;

public class FranchiseConfiguration : IEntityTypeConfiguration<Franchise>
{
    public void Configure(EntityTypeBuilder<Franchise> builder)
    {
        builder.ToTable("franchise");

        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ViewingOrder).HasColumnName("viewing_order");
        builder.Property(x => x.ViewingYear).HasColumnName("viewing_year");
    }
}