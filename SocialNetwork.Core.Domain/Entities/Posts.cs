using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Posts: AuditableEntityWithId
    {
        public string Image { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime DateOfCreated { get; set; }
        //Navegation Properties
        public string IdUser { get; set; } = null!;
        public ICollection<Comments> Comments { get; set; }

    }
}
