namespace SocialNewtwork.Core.Application.Dtos.Account
{
    public class AuthenticationReponse
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Email { get; set; } = null!;

        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; } 
        public bool HasError { get; set; } 
        public string Error { get; set; } = null!;
    }
}
