namespace MiniCRM.Services.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file, string path);

        bool IsValidFile(IFormFile file);

    }
}
