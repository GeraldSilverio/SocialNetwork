namespace SocialNetwork.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
