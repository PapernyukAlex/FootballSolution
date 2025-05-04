using FootballShared.Models;
using FootballShared.Repositories;
using Microsoft.Extensions.Logging;

namespace FootballExtractor
{
    internal class Job(FootballApi footballApi, ApiResponseConverter responseConverter,
        UnitOfWork unitOfWork, ILogger<Job> logger)
    {
        private int Season;
        private int CompetitionId;

        internal void Process(int season, int competitionId)
        {
            Season = season;
            CompetitionId = competitionId;
            var teamsIds = processTeams();
            processMatches(teamsIds);
            processTopScorerStatistics();
            processTeamPosition();
        }

        private List<int> processTeams()
        {
            String responseTeams = footballApi.GetAllTeams(CompetitionId, Season);
            var teamsIds = responseConverter.ConvertToTeamsIds(responseTeams);
            var existingTeamIds = unitOfWork.TeamRepository.GetIdsByCompetition(CompetitionId);
            var newTeamIds = teamsIds.Except(existingTeamIds).ToList();
            foreach (var teamId in newTeamIds)
            {
                var responseTeamInfo = footballApi.GetTeamInfo(teamId);
                var team = responseConverter.ConvertToTeam(responseTeamInfo);
                logger.LogInformation($"Adding the team {team.Name}");
                unitOfWork.TeamRepository.Add(team);
                unitOfWork.Save();
            }
            return teamsIds;
        }

        private void processMatches(List<int> teamsIds)
        {
            List<int> existingMatchesIds = unitOfWork.MatchRepository.GetIdsByCompetition(CompetitionId);
            var likelyFinishedMatches = unitOfWork.MatchRepository.GetIdsOfLikelyCompletedMatchesByCompetition(CompetitionId);
            var updateMatches = new List<Match>();
            foreach (var teamId in teamsIds)
            {
                logger.LogInformation($"Adding matches for the team Id: {teamId}");
                var responseMatches = footballApi.GetMatches(CompetitionId, teamId, Season);
                List<Match> matches = responseConverter.ConvertToMatches(responseMatches);
                var newMatches = new List<Match>();
                foreach (var match in matches)
                {
                    if (likelyFinishedMatches.Contains(match.Id)
                        && !updateMatches.Any(m => m.Id == match.Id))
                        updateMatches.Add(match);
                    if (!existingMatchesIds.Contains(match.Id))
                        newMatches.Add(match);
                }
                unitOfWork.MatchRepository.Add(newMatches);
                unitOfWork.MatchRepository.Update(updateMatches);
                existingMatchesIds.AddRange(newMatches.Select(match => match.Id));
            }
            unitOfWork.Save();
        }

        private void processTopScorerStatistics()
        {
            unitOfWork.TopScorerStatisticsRepository.TruncateTableByCompetitionId(CompetitionId);
            String responseTopScorers = footballApi.GetTopGoalStatistics(CompetitionId, Season, 20);
            var topScorersStatistics = responseConverter.ConvertToTopScorerStatistics(responseTopScorers);
            unitOfWork.TopScorerStatisticsRepository.Add(topScorersStatistics);
            unitOfWork.Save();
        }
        private void processTeamPosition() {
            unitOfWork.TeamPositionRepository.TruncateTableByCompetitionId(CompetitionId);
            String responseTeamPositions = footballApi.GetStandings(CompetitionId, Season);
            var teamPositions = responseConverter.ConvertToTeamsPositions(responseTeamPositions);
            unitOfWork.TeamPositionRepository.Add(teamPositions);
            unitOfWork.Save();
        }
    }
}
