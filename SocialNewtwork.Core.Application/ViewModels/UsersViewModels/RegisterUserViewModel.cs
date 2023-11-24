using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.ViewModels.UsersViewModels
{
    public class RegisterUserViewModel
    {
        public string? Id { get; set; }
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
        [RegularExpression(@"^(809|829|849)\d{7}$", ErrorMessage = "El número de teléfono debe comenzar con 809 o 829 o 849 y tener 10 dígitos en total. Ejemplo: 829-882-8744")]
        public string PhoneNumber { get; set; } = null!;
        public string? Image { get; set; } = null!;

        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula, un número y un carácter especial.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password),ErrorMessage ="LAS CONTRASEÑAS NO COINCIDEN")]
        [DataType(DataType.Password)]
       
        [Required(ErrorMessage = "*ESTE CAMPO ES REQUERIDO*")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
