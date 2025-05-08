using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViewingService.Domain.Entities;

namespace ViewingService.Infrastructure.Data.Configurations;

public class AnimeReferenceConfiguration : IEntityTypeConfiguration<AnimeReference>
{
    public void Configure(EntityTypeBuilder<AnimeReference> builder)
    {
        builder.ToTable("anime_reference");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.AnimeId).HasColumnName("anime_id");
        
        builder.HasIndex(x => x.AnimeId);
    }
}