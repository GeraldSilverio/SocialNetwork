using Microsoft.AspNetCore.Identity;
using SocialNetwork.Infraestructure.Identity.Entities;
using SocialNewtwork.Core.Application.Dtos.Account;
using SocialNewtwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Infraestructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<AuthenticationReponse> AuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationReponse response = new();

            //
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registed with{request.Email}";
                return response;
            }

            //Esto se encarga de hacer la validacion de Login, 
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            //Si no se encuentra el usuario con su password,se retorna lo siguiente.
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credentials for {request.Email}";
                return response;
            }

            //Si el usuario no se confirma.
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.Password = user.Password;
            response.Image = user.Image;
            response.LastName = user.LastName;
            response.Name = user.Name;
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        //Metodo registro

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new();
            response.HasError = false;

            //Validando que el userName ni el Email existen.
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Username {request.UserName} is already Taken";
                return response;
            }
            
            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already Taken";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                Image = request.Image,

                //Me quede mapeando el request.
            };

            return response;
        }
    }
}
