using FootballShared.Enums;

namespace FootballShared.Models;
public class TeamPosition
{
    public int CompetitionId { get; set; }
    public Competition? Competition { get; set; }
    public TeamPositionType Type { get; set; }
    public int Position { get; set; }
    public int TeamId { get; set; }
    public Team? Team;
    public int PlayedGames { get; set; }

    public string Form { get; set; }
    public int? Won { get; set; }
    public int? Draw { get; set; }
    public int? Lost { get; set; }
    public int Points { get; set; }
    public int? GoalsFor { get; set; }
    public int? GoalsAgainst { get; set; }
    public int? GoalDifference { get; set; }
}

