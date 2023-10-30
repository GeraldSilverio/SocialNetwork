using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, PostViewModel, Posts>, IPostService
    {
        public readonly IPostRepositoryAsync _postRepository;
        private readonly IAccountService _accountService;
        private readonly IFriendsService _friendsService;

        public PostService(IMapper mapper, IPostRepositoryAsync postRepository, IAccountService accountService, IFriendsService friendsService) : base(mapper, postRepository)
        {
            _postRepository = postRepository;
            _accountService = accountService;
            _friendsService = friendsService;
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

        public async Task<List<PostViewModel>> GetAllByUser(string user)
        {
            var userExis = await _accountService.GetByUsername(user);
            return await _postRepository.GetAllByUserId(userExis.Id);
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

        //Declaro la lista donde se van a guardar todos los post de los amigos
        List<PostViewModel> posts = new List<PostViewModel>();
        public async Task<List<PostViewModel>> GetAllByFriend(string user)
        {
            //Obtengo el usuario en linea.
            var userExis = await _accountService.GetByUsername(user);

            //Obtengo los amigos del usuario.
            var friends = await _friendsService.GetAllByUser(userExis.UserName);

            //Ahora por cada amigo iterare para conseguir los post que tiene ese usuario.
            foreach (var friend in friends)
            {
                var postFriends = await _postRepository.GetAllByUserId(friend.IdFriend);
                //Luego por cada post se ira agregando a esta lista de post, para retornarla.
                foreach (var post in postFriends)
                {
                    posts.Add(post);
                }

            }
            //Algoritmo perfecto.
            return posts.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
