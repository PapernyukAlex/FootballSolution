using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class TeamPositionConfiguration : IEntityTypeConfiguration<TeamPosition>
{
    public void Configure(EntityTypeBuilder<TeamPosition> builder)
    {
        builder.ToTable("team_positions", "football");

        builder.HasKey(tp => new { tp.CompetitionId, tp.Type, tp.TeamId });

        builder.HasOne(tp => tp.Team)
            .WithMany()
            .HasForeignKey(tp => tp.TeamId);

        builder.HasOne(tp => tp.Competition)
            .WithMany()
            .HasForeignKey(tp => tp.CompetitionId);

        // Add indexes for potential performance improvements
        builder.HasIndex(tp => tp.CompetitionId);

        builder.Property(e => e.Type)
            .HasConversion<string>();
    }
}

