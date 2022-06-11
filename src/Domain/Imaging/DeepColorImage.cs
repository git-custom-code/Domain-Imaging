namespace CustomCode.Domain.Imaging;

using Data.Imaging.Memory;

/// <summary>
/// <see cref="IImage"/> implementation for images with 16 bit precision per color value.
/// </summary>
public sealed class DeepColorImage : ImageBase, IImage<ushort>
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="DeepColorImage"/> type.
    /// </summary>
    /// <param name="memory"> The image's memory that contains the pixel data. </param>
    /// <param name="decoratorFactory"> A factory that can be used to create <see cref="IImageDecorator"/> instances. </param>
    public DeepColorImage(IImageMemory memory, IImageDecoratorFactory decoratorFactory)
        : base(memory, decoratorFactory)
    { }

    #endregion
}
