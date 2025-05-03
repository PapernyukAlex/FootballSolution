using Microsoft.EntityFrameworkCore;

namespace FootballShared.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PostgresContext _dbContext;
        private readonly DbSet<T> dbSet;

        public Repository(PostgresContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<T>();
        }

        public virtual void Add(IEnumerable<T> newObjects)
        { 
            dbSet.AddRange(newObjects);
        }

        public virtual List<T> GetAll()
        {
            return dbSet.ToList();
        }
    }
}
