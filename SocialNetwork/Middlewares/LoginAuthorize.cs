using Microsoft.AspNetCore.Mvc.Filters;
using SocialNetwork.Controllers;

namespace SocialNetwork.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidationUserSession _userSession;

        public LoginAuthorize(ValidationUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (LoginController)context.Controller;
                context.Result = controller.RedirectToAction("index", "home");
            }
            else
            {
                await next();
            }
        }
    }
}
