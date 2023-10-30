using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.FriendViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IFriendsService:IGenericService<AddFriendViewModel,FriendViewModel,Friends>
    {
        Task<List<FriendViewModel>> GetAllByUser(string user);
    }
}
