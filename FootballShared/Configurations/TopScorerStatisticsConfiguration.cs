using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class TopScorerStatisticsConfiguration : IEntityTypeConfiguration<TopScorerStatistics>
{
    public void Configure(EntityTypeBuilder<TopScorerStatistics> builder)
    {
        builder.ToTable("top_scorer_statistics", "football");

        builder.HasKey(p => new { p.CompetitionId, p.PersonId, p.TeamId }); // Если вы хотите сделать составной ключ

        builder.HasOne(a => a.Team)
            .WithMany()
            .HasForeignKey(f => f.TeamId);

        builder.HasOne(a => a.Competition)
            .WithMany()
            .HasForeignKey(f => f.CompetitionId);

        builder.HasOne(a => a.Person)
            .WithMany()
            .HasForeignKey(f => f.PersonId);
    }
}

