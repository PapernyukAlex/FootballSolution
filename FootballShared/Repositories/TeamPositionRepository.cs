using FootballShared.Enums;
using FootballShared.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballShared.Repositories;

public class TeamPositionRepository : Repository<TeamPosition>
{
    public TeamPositionRepository(PostgresContext dbContext) : base(dbContext) { }

    public List<Competition> GetCompetitions()
    {
        return _dbContext.TeamPositions
            .Where(tp => tp.Competition != null)
            .Select(tp => tp.Competition!)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
            .ToList();
    }
    public List<TeamPosition> GetByCompetitionAndType(int competitionId, 
        TeamPositionType teamPositionType)
    {
        return _dbContext.TeamPositions
            .Include(tp => tp.Competition)
            .Include(tp => tp.Team)
            .Where(tp => tp.CompetitionId == competitionId && tp.Type == teamPositionType)
            .OrderBy(tp => tp.Position)
            .ToList();
    }

    public void TruncateTableByCompetitionId(int CompetitionId)
    {
        _dbContext.Database.ExecuteSql($@"
            delete from football.team_positions
            where competition_id = {CompetitionId}");
    }
}
