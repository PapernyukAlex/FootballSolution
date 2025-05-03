using FootballShared.Enums;
using System;
using System.Collections.Generic;

namespace FootballShared.Models;

public partial class Person
{
    public int Id { get; set; }

    public int CurrentTeamId { get; set; }

    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; } = null;

    public string Nationality { get; set; } = null!;
  
    public DateOnly? ContractStartDate { get; set; } = null;

    public DateOnly? ContractEndDate { get; set; } = null;

    public Role Role { get; set; } = Role.Player;

    public virtual Team CurrentTeam { get; set; } = null!;

    public override string ToString()
    {
        return $"Person: {Name} ({Position})\n" +
               $"ID: {Id}, Team ID: {CurrentTeamId}\n" +
               $"Date of Birth: {(DateOfBirth.HasValue ? DateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A")}\n" +
               $"Nationality: {Nationality}\n" +
               $"Contract: {(ContractStartDate.HasValue ? ContractStartDate.Value.ToString("yyyy-MM") : "N/A")} " +
               $"- {ContractEndDate}\n" +
               $"Role: {Role}\n" +
               $"Current Team: {CurrentTeam?.Name} ({CurrentTeam?.ShortName})";
    }
}
