using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.FriendViewModels;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IPostService:IGenericService<SavePostViewModel,PostViewModel,Posts>,IUploadFile
    {
        Task<List<PostViewModel>> GetAllByUser(string user);
        Task<List<FriendsPostViewModel>> GetAllByFriend(string idFriend);
    }
}
