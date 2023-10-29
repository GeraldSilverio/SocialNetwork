using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IPostService:IGenericService<SavePostViewModel,EditPostViewModel,Posts>,IUploadFile
    {
        Task<List<EditPostViewModel>> GetAllByUser(string user);
    }
}
