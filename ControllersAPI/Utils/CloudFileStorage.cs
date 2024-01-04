using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ControllersAPI.Utils;

public sealed class CloudFileStorage(IConfiguration configuration) : IFileStorage
{
    private readonly string _connectionString = configuration.GetConnectionString("AzureConnection")!;

    /// <summary>
    /// Saves a file to the specified container in Azure Blob Storage.
    /// </summary>
    /// <param name="container">The name of the container to save the file in.</param>
    /// <param name="file">The file to be saved.</param>
    /// <returns>The URI of the saved file.</returns>
    public async Task<string> SaveFile(string container, IFormFile file)
    {
        var client = new BlobContainerClient(_connectionString, container);
        await client.CreateIfNotExistsAsync();
        client.SetAccessPolicy(PublicAccessType.Blob);

        string extension = Path.GetExtension(file.FileName);
        string fileName = $"{Guid.NewGuid()}{extension}";

        BlobClient blob = client.GetBlobClient(fileName);
        await blob.UploadAsync(file.OpenReadStream());

        return blob.Uri.ToString();
    }

    /// <summary>
    /// Deletes a file from the cloud storage.
    /// </summary>
    /// <param name="path">The path of the file to delete.</param>
    /// <param name="container">The name of the container where the file is stored.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteFile(string path, string container)
    {
        if (string.IsNullOrEmpty(path))
            return;

        BlobContainerClient client = new BlobContainerClient(_connectionString, container);
        await client.CreateIfNotExistsAsync();

        string fileName = Path.GetFileName(path);
        BlobClient blob = client.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }

    /// <summary>
    /// Edits a file in the specified container by deleting the existing file at the given path and saving the new file.
    /// </summary>
    /// <param name="container">The name of the container where the file is stored.</param>
    /// <param name="file">The new file to be saved.</param>
    /// <param name="path">The path of the existing file to be deleted.</param>
    /// <returns>The URL of the newly saved file.</returns>
    public async Task<string> EditFile(string container, IFormFile file, string path)
    {
        await DeleteFile(path, container);
        return await SaveFile(container, file);
    }
}
