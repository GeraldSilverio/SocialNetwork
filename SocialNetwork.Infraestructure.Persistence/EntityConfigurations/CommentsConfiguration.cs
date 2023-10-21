using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Infraestructure.Persistence.EntityConfigurations
{
    public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.ToTable("Comments");
            //Keys and Restrictions
            builder.HasKey(x => x.Id);
            //Properties
            builder.Property(x=> x.Content).IsRequired();

            //Relationships
            builder.HasOne(x=> x.User)
                .WithMany(x=> x.Comments)
                .HasForeignKey(x=> x.IdUser)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(x=> x.Post)
                .WithMany(x=> x.Comments)
                .HasForeignKey(x=> x.IdPost)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
