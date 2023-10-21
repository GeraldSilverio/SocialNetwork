
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
            builder.HasKey(x => new {x.IdUser,x.IdFriend});
            builder.Property(x => x.UserName).IsRequired();

            //Relationships.
            builder.HasOne(x => x.Users)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
