using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(e => e.Id).HasName("matches_pkey");

        builder.ToTable("matches", "football");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AreaId).HasColumnName("area_id");
        builder.Property(e => e.AwayTeamId).HasColumnName("away_team_id");
        builder.Property(e => e.CompetitionId).HasColumnName("competition_id");
        builder.Property(e => e.FullTimeAway).HasColumnName("full_time_away");
        builder.Property(e => e.FullTimeHome).HasColumnName("full_time_home");
        builder.Property(e => e.HalfTimeAway).HasColumnName("half_time_away");
        builder.Property(e => e.HalfTimeHome).HasColumnName("half_time_home");
        builder.Property(e => e.HomeTeamId).HasColumnName("home_team_id");
        builder.Property(e => e.MatchDatetime)
            //.HasColumnType("timestamp without time zone")
            .HasColumnName("match_datetime");
        builder.Property(e => e.Matchday).HasColumnName("matchday");
        builder.Property(e => e.Status)
            .HasMaxLength(255)
            .HasColumnName("status");
        builder.Property(e => e.Winner)
            .HasMaxLength(255)
            .HasColumnName("winner");

        builder.HasOne(d => d.Area).WithMany(p => p.Matches)
            .HasForeignKey(d => d.AreaId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("matches_fk1");

        builder.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
            .HasForeignKey(d => d.AwayTeamId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("matches_fk7");

        builder.HasOne(d => d.Competition).WithMany(p => p.Matches)
            .HasForeignKey(d => d.CompetitionId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("matches_fk2");

        builder.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
            .HasForeignKey(d => d.HomeTeamId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("matches_fk6");
    }
}

