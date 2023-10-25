using Microsoft.AspNetCore.Http;

namespace SocialNewtwork.Core.Application.Interfaces.Services
{
    public interface IUploadFile
    {
        string UplpadFile(IFormFile file,string userName);
    }
}
