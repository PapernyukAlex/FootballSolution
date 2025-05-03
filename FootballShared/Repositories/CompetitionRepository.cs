using FootballShared.Models;

namespace FootballShared.Repositories;

public class CompetitionRepository : Repository<Competition>
{
    public CompetitionRepository(PostgresContext dbContext) : base(dbContext) { }

}
