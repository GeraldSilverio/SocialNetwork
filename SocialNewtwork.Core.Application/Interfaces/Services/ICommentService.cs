using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.CommentsViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface ICommentService:IGenericService<SaveCommentViewModel,CommetViewModel,Comments>
    {

    }
}
