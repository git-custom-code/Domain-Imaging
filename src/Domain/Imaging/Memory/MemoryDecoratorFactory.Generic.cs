namespace CustomCode.Domain.Imaging.Memory;

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

    /// <inheritdoc cref="IImageDecoratorFactory{T}"/>
    public IMemory<T> Create(IImage image, IImageMemory memory)
    {
        return new MemoryDecorator<T>(memory);
    }

    #endregion
}
