namespace ControllersAPI.Utils
{
    public interface IFileStorage
    {
        Task<string> SaveFile(string containerName, IFormFile file);
    
        Task DeleteFile(string path, string containerName);

        Task<string> EditFile(string containerName, IFormFile file, string path);
    }
}