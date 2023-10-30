using Microsoft.AspNetCore.Mvc;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.FriendViewModels;

namespace SocialNetwork.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFriendsService _friendService;
        private readonly IPostService _postService;

        public FriendsController(IFriendsService friendService, IHttpContextAccessor httpContextAccessor, IPostService postService)
        {
            _friendService = friendService;
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var user = _httpContextAccessor.HttpContext.User.Identity;
            ViewBag.postFriends = await _postService.GetAllByFriend(user.Name);
            ViewBag.Friends = await _friendService.GetAllByUser(user.Name);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AddFriendViewModel model)
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User.Identity;
                if (!ModelState.IsValid)
                {
                    ViewBag.postFriends = await _postService.GetAllByFriend(user.Name);
                    ViewBag.Friends = await _friendService.GetAllByUser(user.Name);
                    return View(model);
                }
                await _friendService.Add(model);
                if (model.HasError == true)
                {
                    ViewBag.postFriends = await _postService.GetAllByFriend(user.Name);
                    ViewBag.Friends = await _friendService.GetAllByUser(user.Name);
                    return View(model);
                }
                return RedirectToRoute(new { controller = "Friends", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _friendService.Delete(id);
                return RedirectToRoute(new {controller ="Friends",action = "Index"});   

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
