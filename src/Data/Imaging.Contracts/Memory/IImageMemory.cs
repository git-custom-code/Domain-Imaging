namespace CustomCode.Data.Imaging.Memory
{
    /// <summary>
    /// Abstraction for a <see cref="byte"/> array that holds an image's pixel data.
    /// </summary>
    public interface IImageMemory
    {
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
        /// Converts a <see cref="ImageMemory"/> to a byte array.
        /// </summary>
        byte[] AsArray();
    }
}