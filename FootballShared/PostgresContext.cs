using System.Collections.Specialized;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using FootballShared.Configurations;
using FootballShared.Models;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace FootballShared;

public partial class PostgresContext : DbContext
{
    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<Models.Match> Matches { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TopScorerStatistics> TopScorerStatistics { get; set; }

    public virtual DbSet<TeamPosition> TeamPositions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbConfig = (NameValueCollection)ConfigurationManager.GetSection("DatabaseSettings");
        optionsBuilder.UseNpgsql($"Host={dbConfig["Host"]};Username={dbConfig["Username"]};" +
            $"Password={dbConfig["Password"]};Database={dbConfig["Database"]};Port={dbConfig["Port"]}");
        optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new AreaConfiguration());
        modelBuilder.ApplyConfiguration(new MatchConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CompetitionConfiguration());
        modelBuilder.ApplyConfiguration(new TopScorerStatisticsConfiguration());
        modelBuilder.ApplyConfiguration(new TeamPositionConfiguration());

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            if (entity.GetTableName() != null)
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));
            }

            if (entity.GetSchema() != null)
            {
                entity.SetSchema(ToSnakeCase(entity.GetSchema()));
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()));
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                foreignKey.SetConstraintName(ToSnakeCase(foreignKey.GetConstraintName()));
            }
        }

        //modelBuilder.Entity<Team>().ToTable(nameof(Team), t => t.ExcludeFromMigrations());
        //modelBuilder.Entity<Area>().ToTable(nameof(Area), t => t.ExcludeFromMigrations());
        //modelBuilder.Entity<Competition>().ToTable(nameof(Competition), t => t.ExcludeFromMigrations());
        //modelBuilder.Entity<Models.Match>().ToTable(nameof(Models.Match), t => t.ExcludeFromMigrations());
        //modelBuilder.Entity<Person>().ToTable(nameof(Person), t => t.ExcludeFromMigrations());
        //modelBuilder.Entity<Team>().ToTable(nameof(Team), t => t.ExcludeFromMigrations());



        base.OnModelCreating(modelBuilder);
    }

    private static string? ToSnakeCase(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var startUnderscore = Regex.Replace(input, @"(?<=[A-Z])(?=[A-Z][a-z])", "_");
        var camelCaseWithUnderscore = Regex.Replace(startUnderscore, @"(?<=[a-z\d])(?=[A-Z])", "_");
        return camelCaseWithUnderscore.ToLower();
    }

}
