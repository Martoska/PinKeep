using CloudinaryDotNet.Actions;

namespace Course_project.Interfaces
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicUrl);
    }
}
