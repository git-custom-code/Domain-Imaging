namespace CustomCode.Domain.Imaging.Memory
{
    /// <summary>
    /// Abstraction for a <see cref="byte"/> array that holds an image's pixel data.
    /// </summary>
    public interface IImageMemoryBuffer
    {
        /// <summary>
        /// Gets the buffer's number of color channels per pixel.
        /// </summary>
        ColorChannels ColorChannels { get; }

        /// <summary>
        /// Gets the buffer's size in bytes.
        /// </summary>
        ulong Count { get; }

        /// <summary>
        /// Gets the number of bytes per color channel per aligned (padded with zeros to match the alignment criteria) image row. 
        /// </summary>
        uint SizePerAlignedRow { get; }

        /// <summary>
        /// Gets the number of bytes per color channel.
        /// </summary
        ulong SizePerChannel { get; }

        /// <summary>
        /// Converts a <see cref="ImageMemoryBuffer"/> to a byte array.
        /// </summary>
        byte[] AsArray();
    }
}