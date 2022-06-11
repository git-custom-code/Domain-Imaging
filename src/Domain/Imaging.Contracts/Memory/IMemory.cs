namespace CustomCode.Domain.Imaging.Memory;

using System.Collections.Generic;

/// <summary>
/// Interface for an <see cref="IImageDecorator"/> that allows direct access to an image's raw memory.
/// </summary>
public interface IMemory : IImageDecorator
{
    /// <summary>
    /// Gets the image's color channels.
    /// </summary>
    IColorChannelCollection Channels { get; }

    /// <summary>
    /// Gets the image's raw memory as byte array.
    /// </summary>
    /// <returns> The image's raw memory as byte array. </returns>
    byte[] AsArray();

    /// <summary>
    /// Gets the image's raw memory as <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns> An <see cref="IEnumerable{T}"/> over the image's raw memory. </returns>
    IEnumerable<byte> AsEnumerable();
}
