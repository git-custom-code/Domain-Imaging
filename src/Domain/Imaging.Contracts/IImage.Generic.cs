namespace CustomCode.Domain.Imaging;

using System;

/// <summary>
/// Interface for a image that contains pixel data of the specified color depth <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T"> The type of the image's color data (bit, byte or ushort). </typeparam>
public interface IImage<T> : IImage
    where T : struct, IComparable, IConvertible, IFormattable
{ }
