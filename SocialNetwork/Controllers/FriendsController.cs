using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.CommentsViewModels;
using SocialNewtwork.Core.Application.ViewModels.FriendViewModels;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFriendsService _friendService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public FriendsController(IFriendsService friendService, IHttpContextAccessor httpContextAccessor, IPostService postService, ICommentService commentService)
        {
            _friendService = friendService;
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
            _commentService = commentService;
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
        [HttpPost]
        public async Task<IActionResult> Comment(string IdUser, int IdPost, string comment)
        {
            if(comment == null)
            {
                return RedirectToRoute(new { controller = "Friends", action = "Index" });
            }
            var saveComment = new SaveCommentViewModel()
            {
                IdPost = IdPost,
                Content = comment,
                IdUser = IdUser,
            };

           
            await _commentService.Add(saveComment);
            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }
    }
}
