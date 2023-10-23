using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Domain.Settings;
using SocialNetwork.Infraestructure.Shared.Services;
using SocialNewtwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Infraestructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}