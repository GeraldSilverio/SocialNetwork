using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comments:AuditableEntityWithId
    {
        public string Content { get; set; } = null!;
        //Navegation Properties.
        public Users User { get; set; } = null!;
        public int IdUser { get; set; }
        public Posts Post { get; set; } = null!;
        public int IdPost { get; set; }

    }
}
