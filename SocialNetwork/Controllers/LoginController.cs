using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Middlewares;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;
using SocialNewtwork.Core.Application.Helpers;

namespace SocialNetwork.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidationUserSession _validationUserSession;

        public LoginController(IUserService userService, ValidationUserSession validationUserSession)
        {
            _userService = userService;
            _validationUserSession = validationUserSession;
        }

        public IActionResult Index()
        {
            if (_validationUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Index(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userService.LoginAsync(model);

                if(user != null)
                {
                    HttpContext.Session.Set<UserViewModel>("user", user);
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    ModelState.AddModelError("userValidation", "DATOS DE ACCESO INCORRECTOS");
                }
                return View("Index", model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
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
                register.Image = "Hello";
                var registerCreated = await _userService.Add(register);
                if (registerCreated != null && registerCreated.Id != 0)
                {
                    registerCreated.Image = _userService.UplpadFile(register.File, registerCreated.Id);
                    await _userService.Update(registerCreated, registerCreated.Id);
                }
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
