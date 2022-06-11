namespace CustomCode.Domain.Imaging.Memory;

using System;
using System.Collections.Generic;

/// <summary>
/// Interface for an <see cref="IImageDecorator"/> that allows direct access to an image's raw memory.
/// </summary>
/// <typeparam name="T"> The data type of the image's memory. </typeparam>
public interface IMemory<T> : IMemory
    where T : struct, IComparable, IConvertible, IFormattable
{
    /// <summary>
    /// Gets the image's color channels.
    /// </summary>
    new IColorChannelCollection<T> Channels { get; }

    /// <summary>
    /// Gets the image's raw memory as array.
    /// </summary>
    /// <returns> The image's raw memory as array. </returns>
    new T[] AsArray();

    /// <summary>
    /// Gets the image's raw memory as <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns> An <see cref="IEnumerable{T}"/> over the image's raw memory. </returns>
    new IEnumerable<T> AsEnumerable();

    /// <summary>
    /// Gets the image's raw memory as <see cref="System.Memory{T}"/>.
    /// </summary>
    /// <returns> The image's raw memory as <see cref="System.Memory{T}"/>. </returns>
    Memory<T> AsMemory();

    /// <summary>
    /// Gets the image's raw memory as <see cref="System.Span{T}"/>.
    /// </summary>
    /// <returns> The image's raw memory as <see cref="System.Span{T}"/>. </returns>
    Span<T> AsSpan();
}
