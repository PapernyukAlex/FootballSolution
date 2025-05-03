namespace FootballShared.Repositories;
public class UnitOfWork
{
    private readonly PostgresContext _dbcontext;
    public MatchRepository MatchRepository { get; set; }
    public PersonRepository PersonRepository { get; set; }
    public TeamRepository TeamRepository { get; set; }

    public CompetitionRepository CompetitionRepository { get; set; }
    public TopScorerStatisticsRepository TopScorerStatisticsRepository { get; set; }
    public TeamPositionRepository TeamPositionRepository { get; set; }



    public UnitOfWork(PostgresContext dbContext)
    {
        _dbcontext = dbContext;
        TeamRepository = new TeamRepository(dbContext);
        MatchRepository = new MatchRepository(dbContext);
        PersonRepository = new PersonRepository(dbContext);
        CompetitionRepository = new CompetitionRepository(dbContext);
        TopScorerStatisticsRepository = new TopScorerStatisticsRepository(dbContext);
        TeamPositionRepository = new TeamPositionRepository(dbContext);
    }

    public void Save()
    {
        _dbcontext.SaveChanges();
    }
}
