using FootballShared.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace FootballShared.Repositories
{
    public class TeamRepository : Repository<Team>
    {
        public TeamRepository(PostgresContext dbContext) : base(dbContext) { }

        public List<int> GetIdsByCompetition(int competitionId)
        {
            return _dbContext.Teams
                .AsNoTracking()
                .Where(t => t.Competitions.Select(c => c.Id).Contains(competitionId))
                .Select(t => t.Id)
                .ToList();
        }

        public Team GetById(int id)
        {
            return _dbContext.Teams
                .Where(t => t.Id == id)
                .Include(t => t.People)
                .Include(t => t.Competitions)
                .Include(t => t.Area)
                .First();
        }
        public void Add(Team team)
        {
            var existingTeam = _dbContext.Teams.SingleOrDefault(t => t.Id == team.Id);

            var existingArea = _dbContext.Areas.SingleOrDefault(a => a.Id == team.AreaId);

            if (existingArea != null)
            {
                team.Area = existingArea;
            }

            var receivedCompetitions = team.Competitions.Select(c => c.Id).ToList();

            var existingCompetitions = _dbContext.Competitions
                .Where(c => receivedCompetitions.Contains(c.Id))
                .ToList();

            foreach (var existingCompetition in existingCompetitions)
            {
                var recievedCompetition = team.Competitions
                    .FirstOrDefault(c => c.Id == existingCompetition.Id);
                if (recievedCompetition != null)
                {
                    team.Competitions.Remove(recievedCompetition);
                    team.Competitions.Add(existingCompetition);
                }
            }

            _dbContext.Add(team);
        }
    }
}
