namespace CustomCode.Domain.Imaging
{
    using Data.Imaging.Memory;

    /// <summary>
    /// Interface for a factory that can create a single <see cref="IImageDecorator"/> instance.
    /// </summary>
    /// <typeparam name="T"> The type of the decorator to be created. </typeparam>
    public interface IImageDecoratorFactory<T>
        where T : IImageDecorator
    {
        /// <summary>
        /// Create a new <see cref="IImageDecorator"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="image"> The image instance that should be decorated. </param>
        /// <param name="memory"> The image's <see cref="IImageMemory"/> that contains the pixel data. </param>
        /// <returns> The requested image decorator instance. </returns>
        T Create(IImage image, IImageMemory memory);
    }
}