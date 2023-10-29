
using SocialNewtwork.Core.Application.Dtos.Account;


namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationReponse> AuthenticationAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SingOutAsync();
        Task<RegisterRequest> GetByUsername(string username);

    }
}
