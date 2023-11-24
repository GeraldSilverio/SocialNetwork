using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNewtwork.Core.Application.Interfaces.Repositories;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class CommentReposityAsync : GenericRepositoryAsync<Comments>, ICommentRepository
    {
        private readonly ApplicationContext _dbContext;
        public CommentReposityAsync(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Comments> GetAllByPostId(int idPost)
        {
            return _dbContext.Comments.Where(x=> x.IdPost == idPost).ToList(); 
        }
    }
}
