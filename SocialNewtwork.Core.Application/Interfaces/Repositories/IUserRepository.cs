using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepositoryAsync<Users>
    {
        bool ValidateUserName(string userName);
        bool ValidateEmail(string email);
        Task<Users> LoginAsync(LoginViewModel loginView);
    }
}
