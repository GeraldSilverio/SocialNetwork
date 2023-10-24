using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.ViewModels.UsersViewModels
{
    public class ForgotPasswordViewModel
    {

        [Required(ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
