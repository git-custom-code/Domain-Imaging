namespace CustomCode.Data.Imaging;

using Memory;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Interface for a repository to load image files from disk.
/// </summary>
public interface IImageMemoryRepository
{
    /// <summary>
    /// Load an image file from disk.
    /// </summary>
    /// <param name="path"> The full path to the image file. </param>
    /// <param name="alignment"> The desired memory alignment of the loaded <see cref="IImageMemory"/>. </param>
    /// <returns> The loaded <see cref="IImageMemory"/>. </returns>
    IImageMemory LoadFrom(string path, MemoryAlignment? alignment = null);

    /// <summary>
    /// Asynchronously load an image file from disk.
    /// </summary>
    /// <param name="path"> The full path to the image file. </param>
    /// <param name="alignment"> The desired memory alignment of the loaded <see cref="IImageMemory"/>. </param>
    /// <param name="token"> A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation. </param>
    /// <returns> An awaitable task with the loaded <see cref="IImageMemory"/>. </returns>
    Task<IImageMemory> LoadFromAsync(string path, MemoryAlignment? alignment = null, CancellationToken? token = null);
}
