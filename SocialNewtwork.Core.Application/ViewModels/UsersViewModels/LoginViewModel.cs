namespace SocialNewtwork.Core.Application.ViewModels.UsersViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool HasError {  get; set; }
        public string? Error { get; set; }
    }
}
