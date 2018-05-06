namespace CustomCode.Domain.Imaging
{
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
        /// <returns> The loaded <see cref="IImage"/>. </returns>
        IImage LoadFrom(string path);

        /// <summary>
        /// Asynchronously load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <returns> An awaitable task with the loaded <see cref="IImage"/>. </returns>
        Task<IImage> LoadFromAsync(string path);
    }
}