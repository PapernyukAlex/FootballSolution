using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballShared.Models;

namespace FootballShared.Configurations;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(e => e.Id).HasName("person_pkey");

        builder.ToTable("person", "football");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ContractEndDate)
            .HasMaxLength(255)
            .HasColumnName("contract_end_date");
        builder.Property(e => e.ContractStartDate).HasColumnName("contract_start_date");
        builder.Property(e => e.CurrentTeamId).HasColumnName("current_team_id");
        builder.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.Nationality)
            .HasMaxLength(255)
            .HasColumnName("nationality");
        builder.Property(e => e.Position)
            .HasMaxLength(255)
            .HasColumnName("position");
        builder.Property(e => e.Role)
            .HasMaxLength(255)
            .HasConversion<string>()
            .HasColumnName("role");

        builder.HasOne(d => d.CurrentTeam).WithMany(p => p.People)
            .HasForeignKey(d => d.CurrentTeamId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("person_fk1");
    }
}

