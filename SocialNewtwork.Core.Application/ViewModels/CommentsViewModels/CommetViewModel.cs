namespace SocialNewtwork.Core.Application.ViewModels.CommentsViewModels
{
    public class CommetViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; } = null!;

        public string UserName { get; set; } = null!;
        public string UserImage { get; set; } = null!;
        public int IdPost { get; set; }
        public string Content { get; set; } = null!;

    }
}
