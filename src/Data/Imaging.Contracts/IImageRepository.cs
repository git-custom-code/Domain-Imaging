namespace CustomCode.Data.Imaging
{
    using Memory;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for a repository to load image files from disk.
    /// </summary>
    public interface IImageRepository
    {
        /// <summary>
        /// Load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <param name="alignment"> The desired memory alignment of the loaded <see cref="IImageMemory"/>. </param>
        /// <returns> The loaded <see cref="IImageMemory"/>. </returns>
        IImageMemory LoadFrom(string path, MemoryAlignment alignment);

        /// <summary>
        /// Asynchronously load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <param name="alignment"> The desired memory alignment of the loaded <see cref="IImageMemory"/>. </param>
        /// <returns> An awaitable task with the loaded <see cref="IImageMemory"/>. </returns>
        Task<IImageMemory> LoadFromAsync(string path, MemoryAlignment alignment);
    }
}