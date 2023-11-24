namespace SocialNewtwork.Core.Application.ViewModels.CommentsViewModels
{
    public class SaveCommentViewModel
    {
        public string IdUser { get; set; } = null!;
        public int IdPost { get; set; }
        public string Content { get; set; } = null!;
    }
}
