using SocialNewtwork.Core.Application.Helpers;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;
using Microsoft.AspNetCore.Http;


namespace SocialNetwork.Middlewares
{
    public class ValidationUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidationUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        //public bool HasUser()
        //{
            //UserViewModel userViewModel = _contextAccessor.HttpContext.Session.Get<UserViewModel>("User");

           /* if(userViewModel == null)
            {
                return false;
            }
            return true;*/
        //}
    }
}
