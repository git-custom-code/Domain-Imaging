namespace CustomCode.Domain.Imaging
{
    using Data.Imaging.Memory;

    /// <summary>
    /// Interface for a factory that can create <see cref="IImageDecorator"/> instances.
    /// </summary>
    public interface IImageDecoratorFactory
    {
        /// <summary>
        /// Create a new <see cref="IImageDecorator"/> instance of the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"> The type of the <see cref="IImageDecorator"/> to be created. </typeparam>
        /// <param name="image"> The image instance that should be decorated. </param>
        /// <param name="memory"> The image's <see cref="IImageMemory"/> that contains the pixel data. </param>
        /// <returns> The requested image decorator instance. </returns>
        T Create<T>(IImage image, IImageMemory memory) where T : IImageDecorator;
    }
}