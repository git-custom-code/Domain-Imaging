namespace CustomCode.Domain.Imaging.Memory
{
    using Core.Composition;
    using Data.Imaging.Memory;
    using System;

    /// <summary>
    /// Implementation for a factory that can create a single <see cref="IImageDecorator"/> instance.
    /// </summary>
    /// <typeparam name="T"> The type of the decorator to be created. </typeparam>
    [Export(typeof(IImageDecoratorFactory<>))]
    public class MemoryDecoratorFactory<T> : IImageDecoratorFactory<IMemory<T>>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        #region Logic

        /// <summary>
        /// Create a new <see cref="IImageDecorator"/> of type <see cref="IMemory{T}"/>.
        /// </summary>
        /// <param name="image"> The image instance that should be decorated. </param>
        /// <param name="memory"> The image's <see cref="IImageMemory"/> that contains the pixel data. </param>
        /// <returns> The requested image decorator instance. </returns>
        public IMemory<T> Create(IImage image, IImageMemory memory)
        {
            return new MemoryDecorator<T>(memory);
        }

        #endregion
    }
}