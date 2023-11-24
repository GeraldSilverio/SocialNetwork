using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class PostRepositoryAsync : GenericRepositoryAsync<Posts>, IPostRepositoryAsync
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public PostRepositoryAsync(ApplicationContext dbContext, IMapper mapper) : base(dbContext)
        {
            _context = dbContext;
            _mapper = mapper;
        }



        public async Task<List<PostViewModel>> GetAllByUserId(string idUser)
        {
            var post = await _context.Posts.OrderByDescending(p => p.Id)
                .Where(p => p.IdUser == idUser).ToListAsync();

            var postViewModel = _mapper.Map<List<PostViewModel>>(post);

            return postViewModel;

        }
    }
}
