using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.ViewModels.FriendViewModels
{
    public class AddFriendViewModel
    {
        [Required(ErrorMessage ="DEBES ESCRIBIR EL NOMBRE DEL USUARIO")]
        public string UserName { get; set; } = null!;
        public string? IdFriend { get; set; }
        public string? IdUser { get; set; }
        public bool HasError { get; set; } = false;
        public string? Error { get; set; } 
    }
}
