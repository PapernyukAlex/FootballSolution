using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(e => e.Id).HasName("team_pkey");
        builder.ToTable("team", "football");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Address)
            .HasMaxLength(255)
            .HasColumnName("address");
        builder.Property(e => e.AreaId).HasColumnName("area_id");
        builder.Property(e => e.ClubColors)
            .HasMaxLength(255)
            .HasColumnName("club_colors");
        builder.Property(e => e.Founded).HasColumnName("founded");
        builder.Property(e => e.IconLink)
            .HasMaxLength(255)
            .HasColumnName("icon_link");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.ShortName)
            .HasMaxLength(255)
            .HasColumnName("short_name");
        builder.Property(e => e.Stadium)
            .HasMaxLength(255)
            .HasColumnName("stadium");
        builder.Property(e => e.Website)
            .HasMaxLength(255)
            .HasColumnName("website");

        builder.HasOne(d => d.Area).WithMany(p => p.Teams)
            .HasForeignKey(d => d.AreaId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("team_fk1");


        builder.HasMany(t => t.Competitions)
            .WithMany(c => c.Teams)
            .UsingEntity<Dictionary<string, object>>(
                "team_competition",
                j => j.HasOne<Competition>().WithMany().HasForeignKey("competition_id"),
                j => j.HasOne<Team>().WithMany().HasForeignKey("team_id")
            );
    }
}

