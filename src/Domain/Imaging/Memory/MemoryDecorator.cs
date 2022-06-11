namespace CustomCode.Domain.Imaging.Memory;

using Data.Imaging.Memory;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Implementation for an <see cref="IImageDecorator"/> that allows direct access to an image's raw memory.
/// </summary>
public sealed class MemoryDecorator<T> : IMemory<T>
    where T : struct, IComparable, IConvertible, IFormattable
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="MemoryDecorator{T}"/> type.
    /// </summary>
    /// <param name="memory"> The image's <see cref="IImageMemory"/> that contains the pixel data. </param>
    public MemoryDecorator(IImageMemory memory)
    {
        if (memory.Precision == MemoryPrecision.OneBit)
        {
            Channels = new ColorChannelBitCollection(memory) as IColorChannelCollection<T>;
        }
        else
        {
            Channels = new ColorChannelCollection<T>(memory);
        }
        Memory = memory;
    }

    /// <summary>
    /// Gets the image's <see cref="IImageMemory"/> that contains the pixel data.
    /// </summary>
    private IImageMemory Memory { get; }

    #endregion

    #region Data

    /// <inheritdoc cref="IMemory"/>
    IColorChannelCollection IMemory.Channels
    {
        get { return Channels; }
    }

    /// <inheritdoc cref="IMemory{T}"/>
    public IColorChannelCollection<T> Channels { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IMemory"/>
    byte[] IMemory.AsArray()
    {
        return Memory.AsArray();
    }

    /// <inheritdoc cref="IMemory{T}"/>
    public T[] AsArray()
    {
        var span = AsSpan();
        return span.ToArray();
    }

    /// <inheritdoc cref="IMemory"/>
    IEnumerable<byte> IMemory.AsEnumerable()
    {
        return Memory.AsArray();
    }

    /// <inheritdoc cref="IMemory{T}"/>
    public IEnumerable<T> AsEnumerable()
    {
        var span = AsSpan();
        return span.ToArray();
    }

    /// <inheritdoc cref="IMemory{T}"/>
    public Memory<T> AsMemory()
    {
        var array = AsArray();
        return new Memory<T>(array);
    }

    /// <inheritdoc cref="IMemory{T}"/>
    public Span<T> AsSpan()
    {
        var memory = new Memory<byte>(Memory.AsArray());
        var span = MemoryMarshal.Cast<byte, T>(memory.Span);
        return span;
    }

    #endregion
}
