using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comments:AuditableBaseEntity
    {
        public string Content { get; set; } = null!;

        //Navegation Properties.
        public string IdUser { get; set; } = null!;
        public Posts Post { get; set; } = null!;
        public int IdPost { get; set; }

    }
}
