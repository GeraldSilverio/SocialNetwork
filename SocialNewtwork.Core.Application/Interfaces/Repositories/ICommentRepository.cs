using SocialNetwork.Core.Domain.Entities;

namespace SocialNewtwork.Core.Application.Interfaces.Repositories
{
    public interface ICommentRepository:IGenericRepositoryAsync<Comments>
    {
        List<Comments> GetAllByPostId(int idPost);
    }
}
