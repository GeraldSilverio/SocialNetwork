using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Dtos.Email;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class UserServices : GenericService<RegisterUserViewModel, UserViewModel, Users>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserServices(IMapper mapper, IUserRepository userRepository, IEmailService emailService) : base(mapper, userRepository)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        //Metodo Login.
        public async Task<UserViewModel> LoginAsync(LoginViewModel loginView)
        {
            Users user = await _userRepository.LoginAsync(loginView);

            if (user == null)
            {
                return null;
            }
            UserViewModel userVw = new()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Image = user.Image,
            };
            return userVw;
        }

        //Sobrescribiendo el metodo Add,para enviar el mensaje de autenticacion del usuario.
        public override  async Task<RegisterUserViewModel> Add(RegisterUserViewModel model)
        {
           var userVm = await base.Add(model);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = userVm.Email,
                Subject = "Welcome to Gex!",
                Body = $"<h1 style=" + "color: blue;" + ">Welcome to Maycol Social Media App</h1>" +
                   $"<p>Your username is {userVm.UserName}</p> \n" +
                   $"Link de activacion: " +
                   $"https://localhost:9999/User/ActivateUser?key={userVm.ActiveKey}"

            });
            return userVm;
        }

        //Metodo subir archivos.
        public string UplpadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Users/{id}";
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

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
