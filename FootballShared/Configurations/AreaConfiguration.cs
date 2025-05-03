using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.HasKey(e => e.Id).HasName("area_pkey");

        builder.ToTable("area", "football");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.IconLink)
            .HasMaxLength(255)
            .HasColumnName("icon_link");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");

        builder.HasMany(a => a.Teams).WithOne(p => p.Area);
    }
}
