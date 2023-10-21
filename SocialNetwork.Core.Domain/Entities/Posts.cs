using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Posts: AuditableEntityWithId
    {
        public string Image { get; set; } = null!;
        public string Content { get; set; } = null!;

        //Navegation Properties
        public Users User { get; set; } = null!;
        public int IdUser {  get; set; }
        public ICollection<Comments> Comments { get; set; }

    }
}
