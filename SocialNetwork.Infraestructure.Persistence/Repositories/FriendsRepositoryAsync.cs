using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNewtwork.Core.Application.Interfaces.Repositories;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class FriendsRepositoryAsync : GenericRepositoryAsync<Friends>, IFriendRepositotyAsync
    {
        private readonly ApplicationContext _dbContext;
        public FriendsRepositoryAsync(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Friends>> GetAllByUser(string idUser)
        {
            var friends = await _dbContext.Friends.Where(x => x.IdUser == idUser).ToListAsync();
            return friends;
        }

        public async Task<bool> IsFriendAdd(string idUser, string idFriend)
        {
            var friends = await _dbContext.Friends.ToListAsync();

            foreach (var friend in friends)
            {
                if (friend.IdUser == idUser && friend.IdFriend == idFriend)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
