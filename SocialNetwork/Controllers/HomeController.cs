using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.CommentsViewModels;
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
            private readonly ICommentService _commentService;
            public HomeController(IPostService postService, IHttpContextAccessor httpContextAccessor, ICommentService commentService)
            {
                _postService = postService;
                _httpContextAccessor = httpContextAccessor;
                _commentService = commentService;
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

            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    await _postService.Delete(id);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }

            public async Task<IActionResult> Update(int id)
            {
                try
                {
                    var postCreated = await _postService.GetById(id);
                    if (postCreated != null)
                    {
                        return View("Update", postCreated);
                    }
                    return View("Index");
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            [HttpPost]
            public async Task<IActionResult> Update(SavePostViewModel model)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    if(model.File != null)
                    {
                        model.Image = _postService.UplpadFile(model.File,model.IdUser);
                    }

                    await _postService.Update(model,model.Id);
                    return RedirectToRoute(new {controller ="Home", action ="Index"});
                }catch(Exception ex)
                {
                    return View(ex.Message);
                }
            }
            [HttpPost]
            public  async Task<IActionResult> Comment(string IdUser, int IdPost,string comment)
            {
                var saveComment = new SaveCommentViewModel()
                {
                    IdPost = IdPost,
                    Content = comment,
                    IdUser = IdUser,
                };
                await _commentService.Add(saveComment);
                return RedirectToRoute(new {controller ="Home", action ="Index"});
            }
        }
    }
}