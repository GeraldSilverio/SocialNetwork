using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Repositories
{
    public interface IPostRepositoryAsync:IGenericRepositoryAsync<Posts>
    {
        Task<List<EditPostViewModel>> GetAllByUser(string user);
    }
}
