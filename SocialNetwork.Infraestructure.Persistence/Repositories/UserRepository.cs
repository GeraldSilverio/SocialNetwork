using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNewtwork.Core.Application.Helpers;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class UserRepository : GenericRepositoryAsync<Users>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ValidateUserName(string userName)
        {
            var isCreated = _dbContext.Users.Any(x => x.UserName == userName);
            return isCreated;
        }

        public override async Task<Users> AddAsync(Users entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }



        public bool ValidateEmail(string email)
        {
            var isCreated = _dbContext.Users.Any(x => x.Email == email);
            return isCreated;
        }

        public Task<Users> LoginAsync(LoginViewModel loginView)
        {
            throw new NotImplementedException();
        }
    }
}
