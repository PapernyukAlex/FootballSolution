using System.Text.Json.Serialization;

namespace FootballShared.Models;

public partial class Area
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    [JsonPropertyName("flag")]
    public string? IconLink { get; set; } = null;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
