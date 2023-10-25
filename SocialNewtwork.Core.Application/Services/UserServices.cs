using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNewtwork.Core.Application.Dtos.Account;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class UserServices : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserServices(IMapper mapper, IAccountService accountService)
        {

            _accountService = accountService;
            _mapper = mapper;

        }
        //Metodo Login.
        public async Task<AuthenticationReponse> LoginAsync(LoginViewModel request)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(request);
            AuthenticationReponse userResponse = await _accountService.AuthenticationAsync(loginRequest);
            return userResponse;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SingOutAsync();
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterUserViewModel model, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(model);
            return await _accountService.RegisterUserAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userdId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userdId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            ResetPasswordRequest forgotRequest = _mapper.Map<ResetPasswordRequest>(model);
            return await _accountService.ResetPasswordAsync(forgotRequest);
        }

        public async Task<RegisterUserViewModel> GetByUsername(string username)
        {
            var request = await _accountService.GetByUsername(username);
            var user = _mapper.Map<RegisterUserViewModel>(request);
            return user;
        }

        //Metodo subir archivos.
        public string UplpadFile(IFormFile file, string userName)
        {

            string basePath = $"/Images/Users/{userName}";
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
