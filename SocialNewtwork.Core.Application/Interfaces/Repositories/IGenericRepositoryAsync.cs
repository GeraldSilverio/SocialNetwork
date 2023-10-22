using System.Linq.Expressions;

namespace SocialNewtwork.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity,int id);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllWithIncluideAsync(List<string> properties);
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
