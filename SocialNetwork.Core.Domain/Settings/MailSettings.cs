namespace SocialNetwork.Core.Domain.Settings
{
    public class MailSettings
    {
        public string EmailFrom { get; set; } = null!;
        public string SmtpHost { get; set; } = null!;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = null!;
        public string SmtpPass { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
    }
}
