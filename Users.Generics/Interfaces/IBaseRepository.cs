using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Users.Generics.Interfaces
{
    public interface IBaseRepository<TId, TEntity>
    {
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> ToListAsync();
        Task<IEnumerable<TEntity>> ToListAsyncAsNoTracking();
        Task<TEntity> GetByIdAsync(TId id);
        void Remove(TEntity obj);
    }
}
