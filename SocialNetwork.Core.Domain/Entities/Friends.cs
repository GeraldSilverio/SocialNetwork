using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Friends : AuditableBaseEntity
    {
        public string IdFriend { get; set; } = null!;
        public string UserName {  get; set; } = null!;
        public string IdUser { get; set; } = null!;
    }
}
