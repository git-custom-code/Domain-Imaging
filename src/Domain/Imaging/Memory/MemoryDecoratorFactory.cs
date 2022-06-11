namespace CustomCode.Domain.Imaging.Memory;

using Core.Composition;
using Data.Imaging.Memory;

/// <summary>
/// Implementation for a factory that can create a single <see cref="IImageDecorator"/> instance.
/// </summary>
[Export(typeof(IImageDecoratorFactory<IMemory>))]
public class MemoryDecoratorFactory : IImageDecoratorFactory<IMemory>
{
    #region Logic

    /// <inheritdoc cref="IImageDecoratorFactory{T}"/>
    public IMemory Create(IImage image, IImageMemory memory)
    {
        return new MemoryDecorator<byte>(memory);
    }

    #endregion
}
