using System.Collections;

namespace MiniCRM.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using MiniCRM.Services.Contracts;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly string defaultImage = @"https://res.cloudinary.com/dx479nsjv/image/upload/v1608064012/CRMSystem/default-img_rftxia.gif";

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadAsync(IFormFile file, string path)
        {
            if (file == null || !this.IsValidFile(file))
            {
                return this.defaultImage;
            }

            string imageUrl = " ";
            byte[] fileBytes;
            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            fileBytes = stream.ToArray();

            await using var uploadStream = new MemoryStream(fileBytes);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, uploadStream),
                Folder = path,
            };

            var result = await this.cloudinary.UploadAsync(uploadParams);

            imageUrl = result.Url.AbsoluteUri;

            return imageUrl;
        }

        public bool IsValidFile(IFormFile file)
        {
            if (file == null)
            {
                return true;
            }

            var validTypes = new string[]
            {
                "image/x-png", "image/gif", "image/jpeg", "image/jpg", "image/png", "image/svg",
            };

            if (!validTypes.Contains(file.ContentType))
            {
                return false;
            }

            return true;
        }
    }
}
