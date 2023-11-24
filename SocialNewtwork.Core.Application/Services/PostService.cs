using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.FriendViewModels;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, PostViewModel, Posts>, IPostService
    {
        public readonly IPostRepositoryAsync _postRepository;
        private readonly IAccountService _accountService;
        private readonly IFriendsService _friendsService;
        private readonly ICommentService _commentService;

        public PostService(IMapper mapper, IPostRepositoryAsync postRepository, IAccountService accountService, IFriendsService friendsService, ICommentService commentService) : base(mapper, postRepository)
        {
            _postRepository = postRepository;
            _accountService = accountService;
            _friendsService = friendsService;
            _commentService = commentService;
        }

        public override async Task Update(SavePostViewModel model, int id)
        {
            var postCreated = await _postRepository.GetByIdAsync(id);
            var postedit = new Posts()
            {
                Id = id,
                IdUser = model.IdUser,
                Content = model.Content,
                Image = model.Image,
                DateOfCreated = postCreated.DateOfCreated
            };
            await _postRepository.UpdateAsync(postedit, id);
        }

        public override async Task Delete(int id)
        {
            var postcomment =  _commentService.GetAllByPostId(id);

            foreach (var comment in await postcomment)
            {
                await _commentService.Delete(comment.Id);
            }

            await base.Delete(id);
        }

        public async Task<List<PostViewModel>> GetAllByUser(string user)
        {
            var userExis = await _accountService.GetByUsername(user);
            var post = await _postRepository.GetAllByUserId(userExis.Id);
            var postViewModels = new List<PostViewModel>();

            foreach (var p in post)
            {
                var comments = await _commentService.GetAllByPostId(p.Id);
                var postViewModel = new PostViewModel
                {
                    Id = p.Id,
                    DateOfCreated = p.DateOfCreated,
                    Image = p.Image,
                    Content = p.Content,
                    IdUser = p.IdUser,
                    Comments = comments
                };
                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        }


        public string UplpadFile(IFormFile file, string idUser)
        {
            string basePath = $"/Images/Posts/{idUser}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return $"{basePath}/{fileName}";
        }


        public async Task<List<FriendsPostViewModel>> GetAllByFriend(string user)
        {
            List<FriendsPostViewModel> posts = new List<FriendsPostViewModel>();
            var userExis = await _accountService.GetByUsername(user);

            var friends = await _friendsService.GetAllByUser(userExis.UserName);
            foreach (var friend in friends)
            {
                var postFriends = await _postRepository.GetAllByUserId(friend.IdFriend);
                foreach (var post in postFriends)
                {
                    var userExisted = await _accountService.GetById(post.IdUser);
                    var friendPost = new FriendsPostViewModel()
                    {
                        Id = post.Id,
                        Image = post.Image,
                        Content = post.Content,
                        DateOfCreated = post.DateOfCreated,
                        Name = userExisted.Name,
                        LastName = userExisted.LastName,
                        ImageUser = userExisted.Image,
                        UserName = userExisted.UserName,
                        Comments = await _commentService.GetAllByPostId(post.Id)
                    };
                    posts.Add(friendPost);
                }
            }
            return posts.OrderByDescending(x => x.DateOfCreated).ToList();
        }
    }
}
