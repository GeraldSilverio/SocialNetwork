using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNewtwork.Core.Application.ViewModels.PostsViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public DateTime DateOfCreated  { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? Image { get; set; }
    }
}
