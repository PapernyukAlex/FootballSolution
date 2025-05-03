using System.Text.Json.Serialization;

namespace FootballShared.Models;

public partial class Team
{
    public int Id { get; set; }

    public int AreaId { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Website { get; set; } = null!;

    public long Founded { get; set; }

    public string ClubColors { get; set; } = null!;
    [JsonPropertyName("venue")]
    public string Stadium { get; set; } = null!;
    [JsonPropertyName("crest")]
    public string IconLink { get; set; } = null!;

    private Area _area;
    
    public virtual Area Area {
        get => _area;
        set {
            AreaId = value.Id;
            _area = value;
        }
    }

    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    [JsonPropertyName("squad")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
    [JsonPropertyName("runningCompetitions")]
    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();

    public override string ToString()
    {
        return $"Team: {Name} ({ShortName})\n" +
           $"ID: {Id}, Area ID: {AreaId}\n" +
           $"Address: {Address}, Website: {Website}\n" +
           $"Founded: {Founded}, Club Colors: {ClubColors}\n" +
           $"Stadium: {Stadium}, Icon: {IconLink}\n" +
           $"Area.Id: {Area.Id}, Area.Name: {Area.Name}, Area.IconLink: {Area.IconLink}\n" +
           $"Matches Home: {MatchHomeTeams.Count}, Matches Away: {MatchAwayTeams.Count}\n" +
           $"People:{string.Join("\n", People)}\n" +
           $"Competitions: {string.Join("\n", Competitions)}";
    }
}
