using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.EntityConfigurations;

namespace SocialNetwork.Infraestructure.Persistence.Contexts
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        //DbSet
        public DbSet<Users> Users { get; set; }
    }
}
