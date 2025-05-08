using AnimeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeService.Infrastructure.Data.Configurations;

public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
{
    public void Configure(EntityTypeBuilder<Anime> builder)
    {
        builder.ToTable("anime_serial");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.PosterUrl).HasColumnName("poster_url");
        builder.Property(x => x.ReleaseFormat).HasColumnName("release_format");
        builder.Property(x => x.EpisodeCount).HasColumnName("episode_count");
        builder.Property(x => x.FillerCount).HasColumnName("filler_count");
        builder.Property(x => x.Duration).HasColumnName("duration");
        builder.Property(x => x.ReleaseDate).HasColumnName("release_date");
        builder.Property(x => x.FranchiseId).HasColumnName("franchise_id");
        builder.Property(x => x.StudioId).HasColumnName("studio_id");
    }
}