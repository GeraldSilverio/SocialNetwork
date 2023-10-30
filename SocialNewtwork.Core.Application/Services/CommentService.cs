using AutoMapper;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.CommentsViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommetViewModel, Comments>, ICommentService
    {
        public CommentService(IMapper mapper, ICommentRepository commentRepository) : base(mapper, commentRepository)
        {
        }
    }
}
