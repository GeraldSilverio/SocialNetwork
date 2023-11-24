using AutoMapper;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.CommentsViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommetViewModel, Comments>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public CommentService(IMapper mapper, ICommentRepository commentRepository, IAccountService accountService) : base(mapper, commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _accountService = accountService;
        }
        public async Task<List<CommetViewModel>> GetAllByPostId(int idPost)
        {
            List<CommetViewModel> commetViewModels = new();
            var comment = _commentRepository.GetAllByPostId(idPost);
            foreach(var item in comment)
            {
                var user = await _accountService.GetById(item.IdUser);

                var commentUser = new CommetViewModel()
                {
                    Id = item.Id,
                    Content = item.Content,
                    UserImage = user.Image,
                    IdPost = item.IdPost,
                    UserName = user.Name,
                };
                commetViewModels.Add(commentUser);
            }
            return commetViewModels;
        }
    }
}
