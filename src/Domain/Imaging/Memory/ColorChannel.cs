namespace CustomCode.Domain.Imaging.Memory;

using Data.Imaging.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Implementation for image color channel that allow acces to individual <see cref="IColorChannelRow{T}"/>s.
/// </summary>
/// <typeparam name="T"> The color channel's precision. </typeparam>
public class ColorChannel<T> : IColorChannel<T>
    where T : struct, IComparable, IConvertible, IFormattable
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ColorChannel{T}"/> type.
    /// </summary>
    /// <param name="index"> The channel's index related to the associated <paramref name="memory"/>. </param>
    /// <param name="memory"> The associated memory that contains the image's pixel data. </param>
    public ColorChannel(byte index, IImageMemory memory)
    {
        Index = index;
        Memory = memory;
        RowCount = (uint)(memory.SizePerChannel / memory.SizePerAlignedRow);
        Rows = new Lazy<List<IColorChannelRow<T>>>(BuildRows, true);
    }

    #endregion

    #region Data

    /// <inheritdoc cref="IColorChannel" />
    IColorChannelRow IColorChannel.this[uint index]
    {
        get { return this[index]; }
    }

    /// <inheritdoc cref="IColorChannel{T}" />
    public IColorChannelRow<T> this[uint index]
    {
        get { return Rows.Value[(int)index]; }
    }

    /// <summary>
    /// Gets the associated memory that contains the image's pixel data.
    /// </summary>
    protected IImageMemory Memory { get; }

    /// <summary>
    /// Gets the channel's index related to the associated <see cref="Memory"/>.
    /// </summary>
    protected byte Index { get; }

    /// <inheritdoc cref="IColorChannel" />
    public uint RowCount { get; }

    /// <summary>
    /// Gets a collection that contains the color channel's rows.
    /// </summary>
    private Lazy<List<IColorChannelRow<T>>> Rows { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IColorChannel" />
    public Span<TType> AsSpan<TType>()
        where TType : struct, IComparable, IConvertible, IFormattable
    {
        var start = (int)(Index * Memory.SizePerChannel);
        var length = (int)Memory.SizePerAlignedRow;
        var memory = new Memory<byte>(Memory.AsArray(), start, length);
        return MemoryMarshal.Cast<byte, TType>(memory.Span);
    }

    /// <summary>
    /// Build the internal <see cref="IColorChannelRow{T}"/> collection.
    /// </summary>
    /// <returns> The internal <see cref="IColorChannelRow{T}"/> collection. </returns>
    protected virtual List<IColorChannelRow<T>> BuildRows()
    {
        var result = new List<IColorChannelRow<T>>();
        for (var i = 0u; i < RowCount; ++i)
        {
            result.Add(new ColorChannelRow<T>(Index, i, Memory));
        }
        return result;
    }

    /// <inheritdoc cref="IColorChannel{T}" />
    public IEnumerator<IColorChannelRow<T>> GetEnumerator()
    {
        foreach (var row in Rows.Value)
        {
            yield return row;
        }
    }

    /// <inheritdoc cref="IColorChannel" />
    IEnumerator<IColorChannelRow> IEnumerable<IColorChannelRow>.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="IEnumerable" />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        if (Memory.ColorChannels == ColorChannels.Monochrome)
        {
            return $"Monochrome ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
        }
        else if (Memory.ColorChannels == ColorChannels.Gray)
        {
            return $"Gray ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
        }
        else if (Memory.ColorChannels == ColorChannels.GrayAlpha)
        {
            if (Index == 0)
            {
                return $"Gray ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
            }
            return $"Alpha ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
        }
        else if (Memory.ColorChannels == ColorChannels.Rgb)
        {
            if (Index == 0)
            {
                return $"Red ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
            }
            else if (Index == 1)
            {
                return $"Green ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
            }
            return $"Blue ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
        }
        else
        {
            if (Index == 0)
            {
                return $"Red ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
            }
            else if (Index == 1)
            {
                return $"Green ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
            }
            else if (Index == 2)
            {
                return $"Blue ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
            }
            return $"Alpha ({RowCount} row{(RowCount == 1 ? string.Empty : "s")})";
        }
    }

    #endregion
}
