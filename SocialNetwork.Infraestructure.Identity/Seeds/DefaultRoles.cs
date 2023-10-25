using Microsoft.AspNetCore.Identity;
using SocialNetwork.Infraestructure.Identity.Entities;
using SocialNewtwork.Core.Application.Enums;

namespace SocialNetwork.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}
