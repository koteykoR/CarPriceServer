using System;
using System.Threading.Tasks;
using IdentityAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using IdentityAPI.Repository.Contexts;
using System.Linq;
using System.Linq.Expressions;

namespace IdentityAPI.Repository.Implementations
{
    public class DBRepository<T> : IRepository<T> where T : class
    {
        private readonly UserContext _context;
        private readonly DbSet<T> _dbSet;

        public DBRepository(UserContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);

        public async Task<T> FindByIdAsync(object id) => await _dbSet.FindAsync(id);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> FindAll() => _dbSet;

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
