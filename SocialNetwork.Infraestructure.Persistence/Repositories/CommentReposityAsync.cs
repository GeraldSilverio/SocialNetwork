using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNewtwork.Core.Application.Interfaces.Repositories;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class CommentReposityAsync : GenericRepositoryAsync<Comments>, ICommentRepository
    {
        public CommentReposityAsync(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
