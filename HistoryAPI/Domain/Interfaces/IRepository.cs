using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace HistoryAPI.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T entity);

        IQueryable<T> FindAll();

        IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);

        Task SaveAsync();
    }
}
