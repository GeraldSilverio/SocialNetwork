using Microsoft.EntityFrameworkCore;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using System.Linq.Expressions;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<TEntity> : IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _dbContext;

        public GenericRepositoryAsync(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Any(predicate);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
             _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async  Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllWithIncluideAsync(List<string> properties)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            foreach(var property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task UpdateAsync(TEntity entity, int id)
        {
            var entry = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
