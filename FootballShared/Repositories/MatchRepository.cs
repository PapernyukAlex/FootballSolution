using FootballShared.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballShared.Repositories
{
    public class MatchRepository : Repository<Match>
    {
        public MatchRepository(PostgresContext dbContext) : base(dbContext) { }

        public List<int> GetIdsByCompetition(int competitionId)
        {
            return _dbContext.Matches
                .AsNoTracking()
                .Where(m => m.CompetitionId == competitionId)
                .Select(m => m.Id)
                .ToList();
        }

        public DateTime? GetMinDateNotFinishedMatchInDb(int competitionId)
        {
            return _dbContext.Matches
                .Where(m => m.CompetitionId == competitionId
                        && m.MatchDatetime < DateTime.UtcNow && m.Status != "FINISHED")
                .Select(m => (DateTime?)m.MatchDatetime.Date)
                .Min();
        }

        public List<Competition> GetCompetitions()
        {
            return _dbContext.Matches
                .Where(tp => tp.Competition != null)
                .Select(tp => tp.Competition!)
                .GroupBy(c => c.Id)
                .Select(g => g.First())
                .ToList();
        }


        public int GetCurrentMatchdayByCompetitionId(int competitionId)
        {
            var query = _dbContext.Matches
                .Where(m => m.CompetitionId == competitionId)
                .GroupBy(m => m.Matchday)
                .Select(g => new
                {
                    Matchday = g.Key,
                    MinTime = g.Min(m => m.MatchDatetime)
                })
                .Where(m => m.MinTime < DateTime.UtcNow);
            return query.Max(m => m.Matchday);
        }

        public List<Match> GetByCompetitionIdAndMatchdate(int commpetitionId, int matchdate)
        {
            return _dbContext.Matches
                .Where(m => (m.CompetitionId == commpetitionId) && (m.Matchday == matchdate))
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToList();
        }

        public List<int> GetAllMatchDays(int competitionId)
        {
            return _dbContext.Matches.Where(m => m.CompetitionId == competitionId)
                    .Select(m => m.Matchday).Distinct().ToList();
        }

        public override void Add(IEnumerable<Match> matches)
        {
            foreach (var match in matches)
            {
                match.Area = null;
                match.HomeTeam = null;
                match.AwayTeam = null;
                match.Competition = null;
            }

            base.Add(matches);
        }

        public void Update(List<Match> matches)
        {
            foreach (var match in matches)
                _dbContext.Entry(match).State = EntityState.Modified;
        }
    }
}
