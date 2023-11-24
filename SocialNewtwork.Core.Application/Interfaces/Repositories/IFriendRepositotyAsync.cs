using SocialNetwork.Core.Domain.Entities;

namespace SocialNewtwork.Core.Application.Interfaces.Repositories
{
    public interface IFriendRepositotyAsync:IGenericRepositoryAsync<Friends>
    {
        Task<List<Friends>> GetAllByUser(string user);
        Task<bool>IsFriendAdd(string idUser, string idFriend);
    }
}
