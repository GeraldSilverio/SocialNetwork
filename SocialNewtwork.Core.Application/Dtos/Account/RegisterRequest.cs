using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Compare(nameof(Password), ErrorMessage = "LAS CONTRASEÑAS NO COINCIDEN")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        [Required(ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
