namespace CustomCode.Domain.Imaging.Memory.Extensions;

using System;
using System.Collections.Generic;

/// <summary>
/// Memory related extension methods for <see cref="IImage"/> types.
/// </summary>
public static class IImageExtensions
{
    #region Logic

    /// <summary>
    /// Gets the image's raw memory as byte array.
    /// </summary>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> The image's raw memory as byte array. </returns>
    public static byte[] AsArray(this IImage image)
    {
        var memory = image.As<IMemory>();
        return memory.AsArray();
    }

    /// <summary>
    /// Gets the image's raw memory as array.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> The image's raw memory as array. </returns>
    public static T[] AsArray<T>(this IImage<T> image)
        where T : struct, IComparable, IConvertible, IFormattable
    {
        var memory = image.As<IMemory<T>>();
        return memory.AsArray();
    }

    /// <summary>
    /// Gets the image's raw memory as <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> An <see cref="IEnumerable{T}"/> over the image's raw memory. </returns>
    public static IEnumerable<byte> AsEnumerable(this IImage image)
    {
        var memory = image.As<IMemory>();
        return memory.AsEnumerable();
    }

    /// <summary>
    /// Gets the image's raw memory as <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> An <see cref="IEnumerable{T}"/> over the image's raw memory. </returns>
    public static IEnumerable<T> AsEnumerable<T>(this IImage<T> image)
        where T : struct, IComparable, IConvertible, IFormattable
    {
        var memory = image.As<IMemory<T>>();
        return memory.AsEnumerable();
    }

    /// <summary>
    /// Gets the image's raw memory as <see cref="System.Memory{T}"/>.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> The image's raw memory as <see cref="System.Memory{T}"/>. </returns>
    public static Memory<T> AsMemory<T>(this IImage<T> image)
        where T : struct, IComparable, IConvertible, IFormattable
    {
        var memory = image.As<IMemory<T>>();
        return memory.AsMemory();
    }

    /// <summary>
    /// Gets the image's raw memory as <see cref="System.Span{T}"/>.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> The image's raw memory as <see cref="System.Span{T}"/>. </returns>
    public static Span<T> AsSpan<T>(this IImage<T> image)
        where T : struct, IComparable, IConvertible, IFormattable
    {
        var memory = image.As<IMemory<T>>();
        return memory.AsSpan();
    }

    /// <summary>
    /// Gets access to an <see cref="IImage{T}"/>'s color channels.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> The <paramref name="image"/>'s color channels. </returns>
    public static IColorChannelCollection<T> Channels<T>(this IImage<T> image)
        where T : struct, IComparable, IConvertible, IFormattable
    {
        var memory = image.As<IMemory<T>>();
        return memory.Channels;
    }

    /// <summary>
    /// Gets access to an <see cref="IImage"/>'s color channels.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> The <paramref name="image"/>'s color channels. </returns>
    public static IColorChannelCollection Channels(this IImage image)
    {
        var memory = image.As<IMemory>();
        return memory.Channels;
    }

    /// <summary>
    /// Gets an <see cref="IImageDecorator"/> that allows direct access to an image's raw memory.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> An <see cref="IImageDecorator"/> that allows direct access to an image's raw memory. </returns>
    public static IMemory<T> Memory<T>(this IImage<T> image)
        where T : struct, IComparable, IConvertible, IFormattable
    {
        var memory = image.As<IMemory<T>>();
        return memory;
    }

    /// <summary>
    /// Gets an <see cref="IImageDecorator"/> that allows direct access to an image's raw memory.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    /// <param name="image"> The extended image instance. </param>
    /// <returns> An <see cref="IImageDecorator"/> that allows direct access to an image's raw memory. </returns>
    public static IMemory Memory(this IImage image)
    {
        var memory = image.As<IMemory>();
        return memory;
    }

    #endregion
}
