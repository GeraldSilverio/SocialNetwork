using AutoMapper;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using System.Linq.Expressions;

namespace SocialNewtwork.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Model> : IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryAsync<Model> _genericRepository;

        public GenericService(IMapper mapper, IGenericRepositoryAsync<Model> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel model)
        {
            var entity = _mapper.Map<Model>(model);
            entity = await _genericRepository.AddAsync(entity);
            SaveViewModel entityvm = _mapper.Map<SaveViewModel>(entity);
            return entityvm;
        }

        public virtual bool Any(Expression<Func<Model, bool>> predicate)
        {
            return _genericRepository.Any(predicate);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            await _genericRepository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entities = await _genericRepository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }

        public virtual async Task<SaveViewModel> GetById(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task Update(SaveViewModel model, int id)
        {
            Model entity = _mapper.Map<Model>(model);
            await _genericRepository.UpdateAsync(entity, id);
        }
    }
}
