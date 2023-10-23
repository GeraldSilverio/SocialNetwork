using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.ViewModels.UsersViewModels
{
    public class RegisterUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.Text)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
        public string? Image { get; set; } = null!;

        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password),ErrorMessage ="LAS CONTRASEÑAS NO COINCIDEN")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public string? ActiveKey { get; set; }
    }
}
