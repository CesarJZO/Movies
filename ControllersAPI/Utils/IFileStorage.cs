namespace ControllersAPI.Utils;
/// <summary>
/// Represents a file storage service that interacts with Azure Blob Storage.
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Saves a file to the specified container in Azure Blob Storage.
    /// </summary>
    /// <param name="container">The name of the container to save the file in.</param>
    /// <param name="file">The file to be saved.</param>
    /// <returns>The URI of the saved file.</returns>
    Task<string> SaveFile(string container, IFormFile file);

    /// <summary>
    /// Deletes a file from the cloud storage.
    /// </summary>
    /// <param name="path">The path of the file to delete.</param>
    /// <param name="container">The name of the container where the file is stored.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteFile(string path, string container);

    /// <summary>
    /// Edits a file in the specified container by deleting the existing file at the given path and saving the new file.
    /// </summary>
    /// <param name="container">The name of the container where the file is stored.</param>
    /// <param name="file">The new file to be saved.</param>
    /// <param name="path">The path of the existing file to be deleted.</param>
    /// <returns>The URL of the newly saved file.</returns>
    Task<string> EditFile(string container, IFormFile file, string path);
}
