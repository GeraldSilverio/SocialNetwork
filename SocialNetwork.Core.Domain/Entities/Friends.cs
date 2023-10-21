using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Friends : AuditableBaseEntity
    {
        public int IdFriend { get; set; }
        public string UserName {  get; set; } = null!;
        public Users Users { get; set; }
        public int IdUser { get; set; }
    }
}
