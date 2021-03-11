using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HistoryAPI.Domain.Interfaces;
using HistoryAPI.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Repository.Implementations
{
    public class DBRepository<T> : IRepository<T> where T : class
    {
        private readonly HistoryContext _context;
        private readonly DbSet<T> _dbSet;

        public DBRepository(HistoryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);

        public IQueryable<T> FindAll() => _dbSet;

        public IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        #region IDisposable implementations
        private bool isDisposed = false;

        protected virtual void Dispose(bool isDispose)
        {
            if (!isDisposed)
            {
                if (isDispose)
                {
                    _context?.Dispose();
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
