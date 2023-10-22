using Microsoft.AspNetCore.Http;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IUploadFile
    {
        string UplpadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "");
    }
}
