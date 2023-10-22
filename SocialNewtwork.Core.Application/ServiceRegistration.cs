using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNewtwork.Core.Application.Interfaces.Services;
using System.Reflection;

namespace SocialNewtwork.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(IGenericService<,,>));
            #endregion
        }
    }
}