using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Core;

namespace UserProtection.Infrastructure.Repositories.Cores
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly UserProtectionContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(UserProtectionContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task AddAsync(TEntity entity) =>
            await _dbSet.AddAsync(entity);

        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();
    }
}