using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    namespace SocialNetwork.Controllers
    {
        [Authorize]
        public class HomeController : Controller
        {
            public HomeController()
            {

            }

            public IActionResult Index()
            {

                return View();

            }

        }
    }
}