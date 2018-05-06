namespace CustomCode.Data.Imaging
{
    using Core.Composition;
    using Memory;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation for a repository to load image files from disk.
    /// </summary>
    [Export(typeof(IImageMemoryRepository))]
    public sealed class ImageMemoryRepository : IImageMemoryRepository
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ImageMemoryRepository"/> type.
        /// </summary>
        /// <param name="imageParsers"> A collection with supported image file parsers. </param>
        public ImageMemoryRepository(IEnumerable<IImageFileParser> imageParsers)
        {
            ImageParsers = imageParsers;
        }

        /// <summary>
        /// Gets a collection with supported image file parsers.
        /// </summary>
        private IEnumerable<IImageFileParser> ImageParsers { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <param name="alignment"> The desired memory alignment of the loaded <see cref="IImageMemory"/>. </param>
        /// <returns> The loaded <see cref="IImageMemory"/>. </returns>
        public IImageMemory LoadFrom(string path, MemoryAlignment alignment)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Image file not found", path);
            }

            var imageExtension = Path.GetExtension(path);
            var parser = ImageParsers.FirstOrDefault(p => p.CanParse(imageExtension));
            if (parser == null)
            {
                throw new NotSupportedException($"Image file format {imageExtension} not supported");
            }

            using (var stream = File.OpenRead(path))
            using (var reader = new BinaryReader(stream))
            {
                return parser.Parse(reader, alignment);
            }
        }

        /// <summary>
        /// Asynchronously load an image file from disk.
        /// </summary>
        /// <param name="path"> The full path to the image file. </param>
        /// <param name="alignment"> The desired memory alignment of the loaded <see cref="IImageMemory"/>. </param>
        /// <returns> An awaitable task with the loaded <see cref="IImageMemory"/>. </returns>
        public async Task<IImageMemory> LoadFromAsync(string path, MemoryAlignment alignment)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Image file not found", path);
            }

            var imageExtension = Path.GetExtension(path);
            var parser = ImageParsers.FirstOrDefault(p => p.CanParse(imageExtension));
            if (parser == null)
            {
                throw new NotSupportedException($"Image file format {imageExtension} not supported");
            }

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous))
            using (var reader = new BinaryReader(stream))
            {
                var memory = await parser.ParseAsync(reader, alignment);
                return memory;
            }
        }

        #endregion
    }
}