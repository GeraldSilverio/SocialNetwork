using Microsoft.AspNetCore.Mvc;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;
using SocialNewtwork.Core.Application.Helpers;
using SocialNewtwork.Core.Application.Dtos.Account;

namespace SocialNetwork.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                AuthenticationReponse user = await _userService.LoginAsync(model);

                if (user != null && user.HasError != true)
                {
                    HttpContext.Session.Set<AuthenticationReponse>("user", user);
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    model.HasError = user.HasError;
                    model.Error = user.Error;
                    return View("Index", model);
                }

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(register);
                }
                var origin = Request.Headers["origin"];
                register.Image = _userService.UplpadFile(register.File, register.UserName);
                RegisterResponse response = await _userService.RegisterAsync(register, origin);
                if (response.HasError)
                {
                    register.Error = response.Error;
                    register.HasError = response.HasError;
                    return View(register);
                }
                return RedirectToRoute(new { controller = "Login", action = "UserCreateConfirm" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var origin = Request.Headers["origin"];
            var response = await _userService.ForgotPasswordAsync(model, origin);

            if (response.HasError)
            {
                model.HasError = response.HasError;
                model.Error = response.Error;
                return View(model);
            }

            return RedirectToRoute(new { controller = "Login", action = "ResetPasswordConfirm" });
        }

        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordPost(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _userService.ResetPasswordAsync(model);
            if (response.HasError)
            {
                model.HasError = response.HasError;
                model.Error = response.Error;
                return View(model);
            }
            return RedirectToRoute(new { controller = "Login", action = "PasswordChangeConfirm" });
        }

        public IActionResult UserCreateConfirm()
        {
            return View();
        }

        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }
        public IActionResult PasswordChangeConfirm()
        {
            return View();
        }
    }
}
