namespace CustomCode.Data.Imaging.Memory;

using System;

/// <summary>
/// Abstraction for a <see cref="byte"/> array that holds an image's pixel data.
/// </summary>
public interface IImageMemory
{
    /// <summary>
    /// Gets the memory's alignment.
    /// </summary>
    /// <remarks>
    /// Image memory can have no alignment at all or be aligned at 4 or 8 byte boundaries. If memoryF is aligned,
    /// each row of an image is padded with zeros so that the length of the row is a multiple of 4 or 8.
    /// </remarks>
    MemoryAlignment Alignment { get; }

    /// <summary>
    /// Gets the memory's number of color channels per pixel.
    /// </summary>
    ColorChannels ColorChannels { get; }

    /// <summary>
    /// Gets the memory's precision, i.e. the number of bits per color channel per pixel.
    /// </summary>
    MemoryPrecision Precision { get; }

    /// <summary>
    /// Gets the memory's size in bytes.
    /// </summary>
    uint Size { get; }

    /// <summary>
    /// Gets the number of bytes per color channel per aligned (padded with zeros to match the alignment criteria) image row. 
    /// </summary>
    uint SizePerAlignedRow { get; }

    /// <summary>
    /// Gets the number of bytes per color channel.
    /// </summary>
    uint SizePerChannel { get; }

    /// <summary>
    /// Gets the number of bytes per pixel.
    /// </summary>
    /// <remarks>
    /// Note that for monochrome memory this field stores the number of bits per unaligned image row 
    /// instead of the number of bytes per pixel (this information would be lost otherwise).
    /// </remarks>
    uint SizePerPixel { get; }

    /// <summary>
    /// Gets the stride per color channel per image row.
    /// The stride is the "number of zero bytes" that are needed to align an image row at 4 or 8 byte boundaries.
    /// </summary>
    byte Stride { get; }

    /// <summary>
    /// Converts a <see cref="IImageMemory"/> to a byte array.
    /// </summary>
    byte[] AsArray();

    /// <summary>
    /// Converts an <see cref="IImageMemory"/> to a <see cref="Memory{T}"/> of bytes.
    /// </summary>
    Memory<byte> AsMemory();

    /// <summary>
    /// Converts an <see cref="IImageMemory"/> to a <see cref="ReadOnlyMemory{T}"/> of bytes.
    /// </summary>
    ReadOnlyMemory<byte> AsReadOnlyMemory();

    /// <summary>
    /// Converts an <see cref="IImageMemory"/> to a readonly span of bytes.
    /// </summary>
    ReadOnlySpan<byte> AsReadOnlySpan();

    /// <summary>
    /// Converts an <see cref="IImageMemory"/> to a span of bytes.
    /// </summary>
    Span<byte> AsSpan();
}
