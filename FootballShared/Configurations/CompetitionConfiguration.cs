using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(e => e.Id).HasName("competition_pkey");

        builder.ToTable("competition", "football");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.IconLink)
            .HasMaxLength(255)
            .HasColumnName("icon_link");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}

