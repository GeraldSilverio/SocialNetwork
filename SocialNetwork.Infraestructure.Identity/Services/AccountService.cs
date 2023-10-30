using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SocialNetwork.Infraestructure.Identity.Entities;
using SocialNewtwork.Core.Application.Dtos.Account;
using SocialNewtwork.Core.Application.Dtos.Email;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;
using System.Text;

namespace SocialNetwork.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
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

        #region RegisterAndSingIn
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
            response.Image = user.Image;
            response.LastName = user.LastName;
            response.Name = user.Name;
            response.IsVerified = user.EmailConfirmed;
            return response;
        }

        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            //Validaciones del UserName
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Username {request.UserName} is already Taken";
                return response;
            }

            //Validacion del Email
            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already Taken";
                return response;
            }

            //Mapeando el modelo.
            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                Image = request.Image,
                PhoneNumber = request.PhoneNumber,

            };

            //Creando el usuario 
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
               
                var verificationUri = await SendVerificationEmailUrl(user, origin);
                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Welcome to GeX APP, please confirm your account. Click Here => {verificationUri}",
                    Subject = "Confirm Account"

                });
            }
            else
            {
                response.HasError = true;
                response.Error = "An error occured to register the user.";
                return response;
            }
            return response;
        }
        #endregion


        #region PasswordMethods
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new();
            response.HasError = false;

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"Don't exist account with the Email {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(account, origin);
            await _emailService.SendAsync(new EmailRequest()
            {
                To = account.Email,
                Body = $"Please change your password. Click Here => {verificationUri}",
                Subject = "Reset Password"

            });

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with {request.Email}";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"$An error ocurred while reset password {request.Email}";
                return response;
            }

            return response;
        }

        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            //Codificando el token de autenticacion
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //Creando la ruta.
            var route = "Login/ResetPassword";
            //Contatenando el LocalHost mas la ruta del controller.
            var Uri = new Uri(string.Concat($"{origin}/", route));
            //Creando el mensaje de verificacion.
            //Token de verificacion
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);
            return verificationUrl;
        }

        #endregion


        #region EmailMethods
        private async Task<string> SendVerificationEmailUrl(ApplicationUser user, string origin)
        {

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Login/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);
            return verificationUri;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return $" Account confirmed.You can use GeX";
            }
            else
            {
                return $"$An error ocurred while confirming {user.Email}";
            }
        }

        public async Task<RegisterRequest> GetByUsername(string username)
        {
            var request = await _userManager.FindByNameAsync(username);

            if(request == null)
            {
                return null;
            }
            var user = new RegisterRequest()
            {
                Id = request.Id,
                UserName = request.UserName,
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Image = request.Image,
                Email = request.Email,
            };

            return user;
        }

        #endregion












    }
}
