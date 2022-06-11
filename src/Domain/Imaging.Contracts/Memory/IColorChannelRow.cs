namespace CustomCode.Domain.Imaging.Memory;

using System;
using System.Collections;

/// <summary>
/// Interface for an image color channel row that allow acces to single color values.
/// </summary>
public interface IColorChannelRow : IEnumerable
{
    /// <summary>
    /// Gets the color value at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index"> The color value's index. </param>
    /// <returns> The color value at the specified <paramref name="index"/>. </returns>
    object this[uint index] { get; }

    /// <summary>
    /// Gets the number of stored color values.
    /// </summary>
    uint Count { get; }

    /// <summary>
    /// Convert the channel to a <see cref="Span{TType}"/>.
    /// </summary>
    /// <returns> A <see cref="Span{TType}"/>, i.e. a "managed pointer" to the channel. </returns>
    Span<TType> AsSpan<TType>(bool ignoreStride = true)
        where TType : struct, IComparable, IConvertible, IFormattable;
}
