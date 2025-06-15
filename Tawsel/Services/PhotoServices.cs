using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Tawsel.Helper;
using Tawsel.Interfaces;

namespace Tawsel.Services
{
    public class PhotoServices : IPhotoServices
    {
        private readonly Cloudinary _cloudinary;

        public PhotoServices(IOptions<CloudinarySetting> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }


        public async Task<ImageUploadResult> AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
                return uploadResult;
            }
            else
            {
                return null;
            }
        }


        public async Task<DeletionResult> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }

    }

}

