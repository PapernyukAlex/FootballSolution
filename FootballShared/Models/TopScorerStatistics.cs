namespace FootballShared.Models;
public class TopScorerStatistics
{
    public int CompetitionId { get; set; }
    public Competition? Competition { get; set; }
    public int PersonId { get; set; }
    public Person? Person;
    public int TeamId { get; set; }
    public Team? Team;
    public int? PlayedMatches { get; set; }
    public int Goals { get; set; }
    public int? Assists { get; set; }
    public int? Penalties { get; set; }
}
