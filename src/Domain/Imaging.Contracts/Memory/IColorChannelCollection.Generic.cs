namespace CustomCode.Domain.Imaging.Memory;

using Data.Imaging.Memory;
using System;
using System.Collections.Generic;

/// <summary>
/// A collection that grants easy access to the individual <see cref="IColorChannel{T}"/>s of an <see cref="IImageMemory"/>.
/// </summary>
/// <typeparam name="T"> The precision of an <see cref="IColorChannel{T}"/> entry. </typeparam>
public interface IColorChannelCollection<T> : IColorChannelCollection, IEnumerable<IColorChannel<T>>
    where T : struct, IComparable, IConvertible, IFormattable
{
    /// <summary>
    /// Gets the <see cref="IColorChannel{T}"/> at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index"> The color channel's index. </param>
    /// <returns> The <see cref="IColorChannel{T}"/> at the specified <paramref name="index"/>. </returns>
    new IColorChannel<T> this[byte index] { get; }
}
