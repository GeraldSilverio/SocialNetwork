using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Infraestructure.Identity.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
    }
}
