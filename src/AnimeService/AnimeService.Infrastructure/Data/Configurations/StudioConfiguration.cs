using AnimeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeService.Infrastructure.Data.Configurations;

public class StudioConfiguration : IEntityTypeConfiguration<Studio>
{
    public void Configure(EntityTypeBuilder<Studio> builder)
    {
        builder.ToTable("studio");
        
        builder.Property(x => x.Name).HasColumnName("name");
    }
}