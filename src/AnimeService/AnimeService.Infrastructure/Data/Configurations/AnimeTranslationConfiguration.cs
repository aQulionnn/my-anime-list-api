using AnimeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeService.Infrastructure.Data.Configurations;

public class AnimeTranslationConfiguration : IEntityTypeConfiguration<AnimeTranslation>
{
    public void Configure(EntityTypeBuilder<AnimeTranslation> builder)
    {
        builder.ToTable("anime_serial_info");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Language).HasColumnName("language");
    }
}