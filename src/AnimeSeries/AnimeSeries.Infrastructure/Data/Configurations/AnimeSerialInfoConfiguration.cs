using AnimeSeries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeSeries.Infrastructure.Data.Configurations;

public class AnimeSerialInfoConfiguration : IEntityTypeConfiguration<AnimeSerialInfo>
{
    public void Configure(EntityTypeBuilder<AnimeSerialInfo> builder)
    {
        builder.ToTable("anime_serial_info");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Language).HasColumnName("language");
        builder.Property(x => x.AnimeSerialId).HasColumnName("anime_serial_id");
    }
}