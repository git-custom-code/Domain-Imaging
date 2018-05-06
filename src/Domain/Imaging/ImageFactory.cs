namespace CustomCode.Domain.Imaging
{
    using Core.Composition;
    using Data.Imaging.Memory;

    /// <summary>
    /// Implementation for a factory that can create <see cref="IImage"/> instances.
    /// </summary>
    [Export(typeof(IImageFactory))]
    public sealed class ImageFactory : IImageFactory
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ImageFactory"/> type.
        /// </summary>
        /// <param name="decoratorFactory"> A factory that can be used to create <see cref="IImageDecorator"/> instances. </param>
        public ImageFactory(IImageDecoratorFactory decoratorFactory)
        {
            DecoratoryFactory = decoratorFactory;
        }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IImageDecorator"/> instances.
        /// </summary>
        private IImageDecoratorFactory DecoratoryFactory { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Create a new <see cref="IImage"/> instance from an already loaded <see cref="IImageMemory"/>.
        /// </summary>
        /// <param name="memory"> The image's already loaded pixel data. </param>
        /// <returns> The newly created image instance. </returns>
        public IImage Create(IImageMemory memory)
        {
            if (memory.Precision == MemoryPrecision.OneBit)
            {
                return new MonochromeImage(memory, DecoratoryFactory);
            }
            else if (memory.Precision == MemoryPrecision.SixteenBit)
            {
                return new DeepColorImage(memory, DecoratoryFactory);
            }
            else
            {
                return new TrueColorImage(memory, DecoratoryFactory);
            }
        }

        #endregion
    }
}