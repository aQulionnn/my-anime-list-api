using AnimeSeries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeSeries.Infrastructure.Data.Configurations;

public class ReWatchedAnimeSerialConfiguration : IEntityTypeConfiguration<ReWatchedAnimeSerial>
{
    public void Configure(EntityTypeBuilder<ReWatchedAnimeSerial> builder)
    {
        builder.ToTable("re_watched_anime_serial");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ReWatchedEpisodes).HasColumnName("re_watched_episodes");
        builder.Property(x => x.ViewingOrder).HasColumnName("viewing_order");
        builder.Property(x => x.AnimeSerialId).HasColumnName("anime_serial_id");
    }
}