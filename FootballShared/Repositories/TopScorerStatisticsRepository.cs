using FootballShared.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballShared.Repositories;

public class TopScorerStatisticsRepository : Repository<TopScorerStatistics>
{
    public TopScorerStatisticsRepository(PostgresContext dbContext) : base(dbContext) {}
    public List<TopScorerStatistics> GetAllByCompetition(int competitionId)
    {
        return _dbContext.TopScorerStatistics
            .Where(tc => tc.CompetitionId == competitionId)
            .Include(tc => tc.Competition)
            .Include(tc => tc.Team)
            .Include(tc => tc.Person)
            .ToList();
    }

    public void TruncateTableByCompetitionId(int CompetitionId)
    {

        _dbContext.Database.ExecuteSql($@"
            delete from football.top_scorer_statistics
            where competition_id = {CompetitionId}");
    }
}
