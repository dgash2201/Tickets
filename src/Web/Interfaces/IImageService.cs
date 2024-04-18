namespace Web.Interfaces
{
    public interface IImageService
    {
        Task Save(IFormFile file);
    }
}