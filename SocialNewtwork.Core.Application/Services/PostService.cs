using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Dtos.Account;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, EditPostViewModel, Posts>, IPostService
    {
        public readonly IPostRepositoryAsync _postRepository;
        private readonly IAccountService _accountService;

        public PostService(IMapper mapper, IPostRepositoryAsync postRepository, IAccountService accountService) : base(mapper, postRepository)
        {
            _postRepository = postRepository;
            _accountService = accountService;
        }

        public async Task<List<EditPostViewModel>> GetAllByUser(string user)
        {
            var userExis = await _accountService.GetByUsername(user);
            return await _postRepository.GetAllByUser(userExis.Id);
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
    }
}
