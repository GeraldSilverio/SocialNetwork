using SocialNewtwork.Core.Application.Helpers;
using SocialNewtwork.Core.Application.Dtos.Account;

namespace SocialNetwork.Middlewares
{
    public class ValidationUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidationUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationReponse userViewModel = _contextAccessor.HttpContext.Session.Get<AuthenticationReponse>("User");

            if(userViewModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
