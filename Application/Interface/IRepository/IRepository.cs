using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IRepository
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetSingle(Expression<Func<T, bool>> expression);
        public Task AddAsync(T entity);
        public void UpdateAsync(T entity);
        public void RemoveAsync(T entity);
        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        public Task<bool> HasAnyAsync(Expression<Func<T, bool>> expression);
    }
}
