using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Infraestructure.Identity.Contexts;
using SocialNetwork.Infraestructure.Identity.Entities;

namespace SocialNetwork.Infraestructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDataBase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName))
                );
            }
            #endregion

            #region Identity
            //Agregando Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            //Procesos de autenticacion.
            services.AddAuthentication();
            #endregion

        }

    }
}