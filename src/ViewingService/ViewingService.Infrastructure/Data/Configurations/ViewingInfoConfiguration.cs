using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViewingService.Domain.Entities;

namespace ViewingService.Infrastructure.Data.Configurations;

public class ViewingInfoConfiguration : IEntityTypeConfiguration<ViewingInfo>
{
    public void Configure(EntityTypeBuilder<ViewingInfo> builder)
    {
        builder.ToTable("viewing_info");
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ViewingOrder).HasColumnName("viewing_order");
        builder.Property(x => x.ViewingYear).HasColumnName("viewing_year");
        builder.Property(x => x.ReWatched).HasColumnName("re_watched");
        builder.Property(x => x.AnimeId).HasColumnName("anime_id");
    }
}