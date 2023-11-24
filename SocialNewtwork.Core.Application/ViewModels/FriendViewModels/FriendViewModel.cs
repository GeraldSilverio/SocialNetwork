namespace SocialNewtwork.Core.Application.ViewModels.FriendViewModels
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IdFriend { get; set; } = null!;
        public string IdUser { get; set; } = null!;
    }
}
