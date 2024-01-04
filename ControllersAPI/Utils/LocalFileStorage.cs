
namespace ControllersAPI.Utils;

public sealed class LocalFileStorage(
    IWebHostEnvironment webHostEnvironment,
    IHttpContextAccessor httpContextAccessor
) : IFileStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <inheritdoc/>
    public async Task<string> SaveFile(string container, IFormFile file)
    {
        string filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        string folder = Path.Combine(_webHostEnvironment.WebRootPath, container);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, filename);

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        byte[] content = memoryStream.ToArray();

        await File.WriteAllBytesAsync(path, content);

        var request = _httpContextAccessor.HttpContext!.Request;
        string url = $"{request.Scheme}://{request.Host}";
        string pathForDb = Path.Combine(url, container, filename).Replace("\\", "/");
        return pathForDb;
    }

    /// <inheritdoc/>
    public async Task<string> EditFile(string container, IFormFile file, string path)
    {
        // if (!string.IsNullOrEmpty(path))

        await DeleteFile(path, container);
        return await SaveFile(container, file);
    }

    /// <inheritdoc/>
    public Task DeleteFile(string path, string container)
    {
        if (string.IsNullOrEmpty(path))
            return Task.CompletedTask;

        var fileName = Path.GetFileName(path);
        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, container, fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }
}
