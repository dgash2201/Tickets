using Web.Interfaces;

namespace Web.Services
{
    public class ImageService : IImageService
    {
        public async Task Save(IFormFile file)
        {
            var fileName = file.FileName;
            var newFile = new FileStream($"wwwroot/{fileName}", FileMode.Create);
            await file.CopyToAsync(newFile);
        }
    }
}