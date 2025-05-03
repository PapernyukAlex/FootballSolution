using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace FootballShared.Repositories
{
    public interface IRepository<T>
    {
        public List<T> GetAll();

        public void Add(IEnumerable<T> newObjects);
    }
}
