using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityAPI.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T entity);
        Task<T> FindByIdAsync(object id);
        IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll();
        Task SaveAsync();
    }
}
