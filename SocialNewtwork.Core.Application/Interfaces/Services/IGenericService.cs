using System.Linq.Expressions;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel,ViewModel,Model> 
        where SaveViewModel :class
        where ViewModel : class
        where Model : class
    {
        Task Update(SaveViewModel model, int id);
        Task<SaveViewModel> Add(SaveViewModel model);
        Task Delete(int id);
        Task<List<ViewModel>> GetAll();
        Task <SaveViewModel> GetById(int id);
        bool Any(Expression<Func<Model, bool>> predicate);
    }
}
