using FootballShared.Enums;
using FootballShared.Models;
using System.Text.Json;

namespace FootballExtractor
{
    internal class ApiResponseConverter
    {
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        internal Team ConvertToTeam(string response)
        {
            JsonElement rootNode = JsonDocument.Parse(response).RootElement;
            var team = rootNode.Deserialize<Team>(serializerOptions)!;

            var coachNode = rootNode.GetProperty("coach");
            Person coach = coachNode.Deserialize<Person>(serializerOptions)!;
            coach.Role = Role.Coach;
            string? contractStartDate = coachNode.GetProperty("contract").GetProperty("start").GetString();
            coach.ContractStartDate = string.IsNullOrEmpty(contractStartDate) ? null : DateOnly.Parse(contractStartDate);

            string? contractEndDate = coachNode.GetProperty("contract").GetProperty("until").GetString();
            coach.ContractEndDate = string.IsNullOrEmpty(contractEndDate) ? null : DateOnly.Parse(contractEndDate);


            team.People.Add(coach);

            team.People.ToList().ForEach(person => person.CurrentTeamId = team.Id);

            return team;
        }

        internal List<int> ConvertToTeamsIds(string response)
        {
            JsonElement rootNode = JsonDocument.Parse(response).RootElement;
            List<int> TeamIds = [];
            foreach (var teamJson in rootNode.GetProperty("teams").EnumerateArray())
            {
                TeamIds.Add(teamJson.GetProperty("id").GetInt16());
            }
            return TeamIds;
        }

        internal List<Match> ConvertToMatches(string response)
        {
            JsonElement rootNode = JsonDocument.Parse(response).RootElement.GetProperty("matches");
            List<Match> matches = [];
            foreach (var matchJson in rootNode.EnumerateArray())
            {
                var match = matchJson.Deserialize<Match>(serializerOptions)!;
                var scoreJson = matchJson.GetProperty("score");
                match.Winner = scoreJson.GetProperty("winner").GetString();

                if (match.Winner != null) {
                    var fullTimeJson = scoreJson.GetProperty("fullTime");
                    match.FullTimeHome = fullTimeJson.GetProperty("home").GetInt16();
                    match.FullTimeAway = fullTimeJson.GetProperty("away").GetInt16();

                    var halfTimeJson = scoreJson.GetProperty("halfTime");
                    match.HalfTimeHome = halfTimeJson.TryGetProperty("home", out var homeProp) && homeProp.ValueKind != JsonValueKind.Null
                        ? homeProp.GetInt16() : null;
                    match.HalfTimeAway = halfTimeJson.TryGetProperty("away", out var awayProp) && awayProp.ValueKind != JsonValueKind.Null
                        ? homeProp.GetInt16() : null;
                }

                matches.Add(match);
            }
            return matches;
        }

        internal List<TopScorerStatistics> ConvertToTopScorerStatistics(string response)
        {
            JsonElement rootNode = JsonDocument.Parse(response).RootElement;
            var competitionId = rootNode.GetProperty("competition").GetProperty("id").GetInt32();
            var topScorers= new List<TopScorerStatistics>();
            foreach (var scorer in rootNode.GetProperty("scorers").EnumerateArray())
            {
                var topGoalStatistics = scorer.Deserialize<TopScorerStatistics>(serializerOptions)!;
                topGoalStatistics.PersonId = scorer.GetProperty("player").GetProperty("id").GetInt32();
                topGoalStatistics.TeamId = scorer.GetProperty("team").GetProperty("id").GetInt32();
                topGoalStatistics.CompetitionId = competitionId;
                topScorers.Add(topGoalStatistics);
            }
            return topScorers;
        }

        internal List<TeamPosition> ConvertToTeamsPositions(string response)
        {
            JsonElement rootNode = JsonDocument.Parse(response).RootElement;
            var competitionId = rootNode.GetProperty("competition").GetProperty("id").GetInt32();
            var teamPositions = new List<TeamPosition>();
            foreach (var standings in rootNode.GetProperty("standings").EnumerateArray())
            {
                var type = standings.GetProperty("type").GetString()!;
                foreach (var teamPositionJson in standings.GetProperty("table").EnumerateArray())
                {
                    var teamPosition = teamPositionJson.Deserialize<TeamPosition>(serializerOptions)!;
                    teamPosition.TeamId = teamPositionJson.GetProperty("team").GetProperty("id").GetInt32();
                    teamPosition.Type = (TeamPositionType)Enum.Parse(typeof(TeamPositionType), type, true);
                    teamPosition.CompetitionId = competitionId;
                    teamPositions.Add(teamPosition);
                }
            }
            return teamPositions;
        }
    }
}
