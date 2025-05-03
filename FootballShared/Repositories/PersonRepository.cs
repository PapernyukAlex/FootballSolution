using FootballShared.Models;

namespace FootballShared.Repositories;

public class PersonRepository : Repository<Person>
{
    public PersonRepository(PostgresContext dbContext) : base(dbContext) {}
}
