using Application.Interface.IExternalService;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustucture.ExternalService
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly CloudinaryConfig _config;

        public CloudinaryService(IOptions<CloudinaryConfig> config)
        {
            _config = config.Value;
            var account = new Account(_config.CloudName, _config.ApiKey, _config.ApiSecret);
            cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImageFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file));
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParam = new ImageUploadParams
                {

                    File = new FileDescription(file.Name, stream),
                    Folder = $"images"
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParam);
                return uploadResult.SecureUrl.ToString();
            }
        }
    }
}
