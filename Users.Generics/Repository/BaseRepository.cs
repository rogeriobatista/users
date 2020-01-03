using Users.Generics.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Users.Generics.Repository
{
    public class BaseRepository<TId, TEntity>
        where TId : struct
        where TEntity : Entity<TId, TEntity>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
        }

        public async Task AddAsync(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity obj) {
            _dbSet.Update(obj);
            _context.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> ToListAsyncAsNoTracking() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<TEntity> GetByIdAsyncAsNoTracking(TId id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));

        public async Task<IEnumerable<TEntity>> ToListAsync() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetByIdAsync(TId id) => await _dbSet.FirstOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));
    }
}