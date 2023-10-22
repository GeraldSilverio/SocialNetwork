using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IUserService:IGenericService<RegisterUserViewModel,UserViewModel,Users>,IUploadFile
    {
        Task<UserViewModel> LoginAsync(LoginViewModel loginView);
    }
}
