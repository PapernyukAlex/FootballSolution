using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FootballShared.Models;

public partial class Competition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonPropertyName("emblem")]
    public string? IconLink { get; set; } = null;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public override string ToString()
    {
        return $"Competition: {Name} (ID: {Id}), Icon: {IconLink}";
    }
}
