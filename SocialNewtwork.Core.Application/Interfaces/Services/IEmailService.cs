using SocialNewtwork.Core.Application.Dtos.Email;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
