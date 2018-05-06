namespace CustomCode.Domain.Imaging
{
    using Core.Composition;
    using Data.Imaging;
    using Data.Imaging.Memory;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation for a repository to load image files from disk.
    /// </summary>
    [Export(typeof(IImageRepository))]
    public sealed class ImageRepository : IImageRepository
    {
        #region Dependencies

        /// <summary>
        /// Creates a new isntance of the <see cref="ImageRepository"/> type.
        /// </summary>
        /// <param name="memoryRepository"> A repository to load raw image pixel data from disk. </param>
        /// <param name="factory"> A factory that can create <see cref="IImage"/> instances. </param>
        public ImageRepository(IImageMemoryRepository memoryRepository, IImageFactory factory)
        {
            MemoryRepository = memoryRepository;
            Factory = factory;
        }

        /// <summary>
        /// Gets a repository to load raw image pixel data from disk.
        /// </summary>
        private IImageMemoryRepository MemoryRepository { get; }

        /// <summary>
        /// Gets a factory that can create <see cref="IImage"/> instances.
        /// </summary>
        private IImageFactory Factory { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <returns> The loaded <see cref="IImage"/>. </returns>
        public IImage LoadFrom(string path)
        {
            var alignment = MemoryAlignment.At32Bit;
            if (Environment.Is64BitOperatingSystem)
            {
                alignment = MemoryAlignment.At64Bit;
            }
            var memory = MemoryRepository.LoadFrom(path, alignment);
            var image = Factory.Create(memory);
            return image;
        }

        /// <summary>
        /// Asynchronously load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <returns> An awaitable task with the loaded <see cref="IImage"/>. </returns>
        public async Task<IImage> LoadFromAsync(string path)
        {
            var alignment = MemoryAlignment.At32Bit;
            if (Environment.Is64BitOperatingSystem)
            {
                alignment = MemoryAlignment.At64Bit;
            }
            var memory = await MemoryRepository.LoadFromAsync(path, alignment);
            var image = Factory.Create(memory);
            return image;
        }

        #endregion
    }
}