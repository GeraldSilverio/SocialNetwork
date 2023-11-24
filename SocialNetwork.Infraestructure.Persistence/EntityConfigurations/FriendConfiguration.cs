
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Infraestructure.Persistence.EntityConfigurations
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friends>
    {
        public void Configure(EntityTypeBuilder<Friends> builder)
        {
            builder.ToTable("Friends");
            //Keys and Restriction
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired();

           
        }
    }
}
