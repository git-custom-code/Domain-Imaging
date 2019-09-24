namespace CustomCode.Domain.Imaging
{
    using Data.Imaging.Memory;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for images that contain pixel data.
    /// </summary>
    public abstract class ImageBase : IImage
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Image"/> type.
        /// </summary>
        /// <param name="memory"> The image's memory that contains the pixel data. </param>
        /// <param name="decoratorFactory"> A factory that can be used to create <see cref="IImageDecorator"/> instances. </param>
        protected ImageBase(IImageMemory memory, IImageDecoratorFactory decoratorFactory)
        {
            DecoratorFactory = decoratorFactory;
            Memory = memory;
            if (memory.Precision == MemoryPrecision.OneBit)
            {
                Dimension = new Dimension(
                    memory.SizePerPixel,
                    memory.SizePerChannel / memory.SizePerAlignedRow);
            }
            else
            {
                Dimension = new Dimension(
                    memory.SizePerAlignedRow - memory.Stride,
                    memory.SizePerChannel / memory.SizePerAlignedRow);
            }
        }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IImageDecorator"/> instances.
        /// </summary>
        private IImageDecoratorFactory DecoratorFactory { get; }

        /// <summary>
        /// Gets the image's memory that contains the pixel data.
        /// </summary>
        private IImageMemory Memory { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets the image's created decorators.
        /// </summary>
        private HashSet<IImageDecorator> Decorators { get; } = new HashSet<IImageDecorator>();

        /// <summary>
        /// Gets the image's dimension (width and height in pixel).
        /// </summary>
        public Dimension Dimension { get; }

        /// <summary>
        /// Gets a light-weight synchronization object.
        /// </summary>
        private object SyncLock { get; } = new object();

        #endregion

        #region Logic

        /// <summary>
        /// Cast the image to a specific <see cref="IImageDecorator"/> in order to use the decorated image's functionality.
        /// </summary>
        /// <typeparam name="T"> The type of the desired decorator. </typeparam>
        /// <returns> The decorated image. </returns>
        public T As<T>() where T : IImageDecorator
        {
            var decorator = (T)Decorators.FirstOrDefault(f => f is T);
            if (decorator == null)
            {
                lock (SyncLock)
                {
                    decorator = (T)Decorators.FirstOrDefault(f => f is T);
                    if (decorator == null)
                    {
                        decorator = DecoratorFactory.Create<T>(this, Memory);
                        Decorators.Add(decorator);
                    }
                }
            }

            return decorator;
        }

        #endregion
    }
}