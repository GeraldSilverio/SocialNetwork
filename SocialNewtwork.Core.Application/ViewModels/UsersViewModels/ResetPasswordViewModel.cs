using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.ViewModels.UsersViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Debe colocar el correo del usuario")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
