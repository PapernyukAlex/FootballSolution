using System.Text.Json.Serialization;

namespace FootballShared.Models;

public partial class Match
{
    public int Id { get; set; }

    public int AreaId { get; set; }

    public int CompetitionId { get; set; }
    [JsonPropertyName("utcDate")]
    public DateTime MatchDatetime { get; set; }
    public string Status { get; set; } = null!;
    public int Matchday { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public string? Winner { get; set; }

    public int FullTimeHome { get; set; }

    public int FullTimeAway { get; set; }

    public int? HalfTimeHome { get; set; }

    public int? HalfTimeAway { get; set; }

    private Area? _area;

    public virtual Area? Area
    {
        get => _area;
        set
        {
            _area = value;
            if (value != null)
                AreaId = value.Id;
        }
    }

    private Team? _awayTeam;

    public virtual Team? AwayTeam
    {
        get => _awayTeam;
        set
        {
            _awayTeam = value;
            if (value != null)
                AwayTeamId = value.Id;
        }
    }

    private Competition? _competition;

    public virtual Competition? Competition
    {
        get => _competition;
        set
        {
            _competition = value;
            if (value != null)
                CompetitionId = value.Id;
        }
    }

    private Team? _homeTeam;

    public virtual Team? HomeTeam
    {
        get => _homeTeam;
        set
        {
            _homeTeam = value;
            if (value != null)
                HomeTeamId = value.Id;
        }
    }

    public override string ToString()
    {
        string homeTeamName = HomeTeam?.Name ?? "Unknown Home Team";
        string awayTeamName = AwayTeam?.Name ?? "Unknown Away Team";
        string competitionName = Competition?.Name ?? "Unknown Competition";
        string areaName = Area?.Name ?? "Unknown Area";

        string score = Status == "FINISHED"
            ? $"{FullTimeHome} - {FullTimeAway}"
            : "vs";

        string winnerInfo = !string.IsNullOrEmpty(Winner)
            ? $" (Winner: {(Winner == "HOME_TEAM" ? homeTeamName : awayTeamName)})"
            : "";

        return $"{HomeTeamId} {homeTeamName} {score} {AwayTeamId} {awayTeamName}{winnerInfo}\n" +
               //$"Competition: {competitionName}\n" +
               //$"Area: {areaName}\n" +
               //$"Matchday: {Matchday}\n" +
               $"Date: {MatchDatetime:g}\n";
               //$"Status: {Status}\n" +
               //$"Half-time score: {HalfTimeHome} - {HalfTimeAway}";
    }
}
