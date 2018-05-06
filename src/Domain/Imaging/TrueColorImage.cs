namespace CustomCode.Domain.Imaging
{
    using Data.Imaging.Memory;

    /// <summary>
    /// <see cref="IImage"/> implementation for images with 8 bit precision per color value.
    /// </summary>
    public sealed class TrueColorImage : ImageBase, IImage<byte>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="TrueColorImage"/> type.
        /// </summary>
        /// <param name="memory"> The image's memory that contains the pixel data. </param>
        /// <param name="decoratorFactory"> A factory that can be used to create <see cref="IImageDecorator"/> instances. </param>
        public TrueColorImage(IImageMemory memory, IImageDecoratorFactory decoratorFactory)
            : base (memory, decoratorFactory)
        { }

        #endregion
    }
}