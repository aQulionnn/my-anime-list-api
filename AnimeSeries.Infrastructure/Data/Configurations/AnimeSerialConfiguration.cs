using AnimeSeries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeSeries.Infrastructure.Data.Configurations;

public class AnimeSerialConfiguration : IEntityTypeConfiguration<AnimeSerial>
{
    public void Configure(EntityTypeBuilder<AnimeSerial> builder)
    {
        builder.ToTable("anime_serial");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Season).HasColumnName("season");
        builder.Property(x => x.Part).HasColumnName("part");
        builder.Property(x => x.Episodes).HasColumnName("episodes");
        builder.Property(x => x.WatchedEpisodes).HasColumnName("watched_episodes");
        builder.Property(x => x.Fillers).HasColumnName("fillers");
        builder.Property(x => x.ReleaseDate).HasColumnName("release_date");
        builder.Property(x => x.ViewingYear).HasColumnName("viewing_year");
        builder.Property(x => x.ViewingOrder).HasColumnName("viewing_order");
        builder.Property(x => x.PosterUrl).HasColumnName("poster_url");
        builder.Property(x => x.FranchiseId).HasColumnName("franchise_id");
    }
}