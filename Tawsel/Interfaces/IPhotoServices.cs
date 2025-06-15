using CloudinaryDotNet.Actions;

namespace Tawsel.Interfaces
{
    public interface IPhotoServices 
    {
        Task<ImageUploadResult> AddPhoto(IFormFile photo);
        Task<DeletionResult> DeletePhoto(string publicId);

    }
}
