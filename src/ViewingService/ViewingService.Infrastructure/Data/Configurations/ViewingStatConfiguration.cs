using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViewingService.Domain.Entities;

namespace ViewingService.Infrastructure.Data.Configurations;

public class ViewingStatConfiguration : IEntityTypeConfiguration<ViewingStat>
{
    public void Configure(EntityTypeBuilder<ViewingStat> builder)
    {
        builder.ToTable("viewing_stat");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.WatchedEpisodes).HasColumnName("watched_episodes");
        builder.Property(x => x.Year).HasColumnName("year");
        builder.Property(x => x.ViewingInfoId).HasColumnName("viewing_info_id");
    }
}