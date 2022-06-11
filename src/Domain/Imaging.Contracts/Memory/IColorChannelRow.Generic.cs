namespace CustomCode.Domain.Imaging.Memory;

using System;
using System.Collections.Generic;

/// <summary>
/// Interface for an image color channel row that allow acces to single color values.
/// </summary>
/// <typeparam name="T"> The color channel row's precision. </typeparam>
public interface IColorChannelRow<T> : IColorChannelRow, IEnumerable<T>
    where T : IComparable, IConvertible, IFormattable
{
    /// <summary>
    /// Gets the color value at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index"> The color value's index. </param>
    /// <returns> The color value at the specified <paramref name="index"/>. </returns>
    new T this[uint index] { get; }
}
