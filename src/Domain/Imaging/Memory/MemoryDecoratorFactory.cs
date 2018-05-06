namespace CustomCode.Domain.Imaging.Memory
{
    using Core.Composition;
    using Data.Imaging.Memory;

    /// <summary>
    /// Implementation for a factory that can create a single <see cref="IImageDecorator"/> instance.
    /// </summary>
    [Export(typeof(IImageDecoratorFactory<IMemory>))]
    public class MemoryDecoratorFactory : IImageDecoratorFactory<IMemory>
    {
        #region Logic

        /// <summary>
        /// Create a new <see cref="IImageDecorator"/> of type <see cref="IMemory"/>.
        /// </summary>
        /// <param name="image"> The image instance that should be decorated. </param>
        /// <param name="memory"> The image's <see cref="IImageMemory"/> that contains the pixel data. </param>
        /// <returns> The requested image decorator instance. </returns>
        public IMemory Create(IImage image, IImageMemory memory)
        {
            return new MemoryDecorator<byte>(memory);
        }

        #endregion
    }
}