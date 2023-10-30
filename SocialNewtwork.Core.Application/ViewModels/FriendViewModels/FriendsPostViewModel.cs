using SocialNewtwork.Core.Application.ViewModels.CommentsViewModels;

namespace SocialNewtwork.Core.Application.ViewModels.FriendViewModels
{
    public class FriendsPostViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Comment { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? ImageUser { get; set; }
        public List<CommetViewModel>? Comments { get; set; }
        public DateTime DateOfCreated { get; set; }
    }
}
