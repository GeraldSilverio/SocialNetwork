using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Middlewares;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;

namespace SocialNetwork.Controllers
{
    namespace SocialNetwork.Controllers
    {
        [Authorize]
        public class HomeController : Controller
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IPostService _postService;
            public HomeController(IPostService postService, IHttpContextAccessor httpContextAccessor)
            {
                _postService = postService;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<IActionResult> Index()
            {
                var user = _httpContextAccessor.HttpContext.User.Identity;

                ViewBag.Posts = await _postService.GetAllByUser(user.Name);
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Index(SavePostViewModel model)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    model.Image = _postService.UplpadFile(model.File, model.IdUser);
                    var post = await _postService.Add(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }

            }

        }
    }
}