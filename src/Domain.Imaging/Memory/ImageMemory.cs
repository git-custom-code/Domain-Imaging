namespace CustomCode.Domain.Imaging.Memory
{
    /// <summary>
    /// Abstraction for a <see cref="byte"/> array that holds an image's pixel data.
    /// </summary>
    /// <remarks>
    /// The image memory supports the following:
    /// - data precision can be stored as 1bit, 8bit or 16bit per color channel per pixel
    /// - data can be stored with 1 - 4 color channels (monochrome, gray, gray alpha, rgb or rgba) per pixel
    /// - data can be aligned at none, 16bit or 32bit boundaries to benefit from memory burst read mode.
    /// </remarks>
    public sealed class ImageMemory : IImageMemory
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ImageMemory"/> type.
        /// </summary>
        /// <param name="dimension"> The memory's dimensions (width and height). </param>
        /// <param name="alignment"> The memory's alignment (none, 32bit or 64bit). </param>
        /// <param name="colorChannels"> The memory's number of color channels (Monochrome, Grey, GreyAlpha, Rgb or Rgba). </param>
        /// <param name="precision"> The memory's precision per color channel per pixel (1bit, 8bit or 16bit). </param>
        public ImageMemory(
            Dimension dimension,
            MemoryAlignment alignment,
            ColorChannels colorChannels,
            MemoryPrecision precision)
        {
            Alignment = alignment;
            ColorChannels = colorChannels;
            Precision = precision;

            SizePerPixel = (uint)((byte)ColorChannels * (byte)precision);
            if (SizePerPixel == 0)
            {
                ColorChannels = ColorChannels.Monochrome;
                Precision = MemoryPrecision.OneBit;
                SizePerPixel = dimension.Width;
                SizePerUnalignedRow = (uint)(dimension.Width / 8.0);
                if (dimension.Width % 8 != 0)
                {
                    ++SizePerUnalignedRow;
                }
            }
            else
            {
                SizePerUnalignedRow = dimension.Width * (uint)(Precision);
            }

            Stride = 0;
            var inverseStride = SizePerUnalignedRow % (uint)Alignment;
            if (inverseStride > 0)
            {
                Stride = (byte)((uint)Alignment - inverseStride);
            }

            SizePerAlignedRow = SizePerUnalignedRow + Stride;
            SizePerChannel = SizePerAlignedRow * dimension.Height;

            var dataCount = SizePerChannel;
            if (ColorChannels != ColorChannels.Monochrome)
            {
                dataCount = (uint)ColorChannels * SizePerChannel;
            }
            Data = new byte[dataCount];
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the memory's alignment.
        /// </summary>
        /// <remarks>
        /// Image memory can have no alignment at all or be aligned at 4 or 8 byte boundaries. If memoryF is aligned,
        /// each row of an image is padded with zeros so that the length of the row is a multiple of 4 or 8.
        /// </remarks>
        public MemoryAlignment Alignment { get; }

        /// <summary>
        /// Gets the memory's number of color channels per pixel.
        /// </summary>
        public ColorChannels ColorChannels { get; }

        /// <summary>
        /// Get the memory's raw data as byte array.
        /// </summary>
        private byte[] Data { get; }

        /// <summary>
        /// Gets the memory's size in bytes.
        /// </summary>
        public ulong Count
        {
            get { return (ulong)Data.LongLength; }
        }

        /// <summary>
        /// Gets the memory's precision, i.e. the number of bits per color channel per pixel.
        /// </summary>
        public MemoryPrecision Precision { get; }

        /// <summary>
        /// Gets the number of bytes per color channel per aligned (padded with zeros to match the alignment criteria) image row. 
        /// </summary>
        public uint SizePerAlignedRow { get; }

        /// <summary>
        /// Gets the number of bytes per color channel.
        /// </summary>
        public ulong SizePerChannel { get; }

        /// <summary>
        /// Gets the number of bytes per pixel.
        /// </summary>
        /// <remarks>
        /// Note that for monochrome memory this field stores the number of bits per unaligned image row 
        /// instead of the number of bytes per pixel (this information would be lost otherwise).
        /// </remarks>
        public uint SizePerPixel { get; }

        /// <summary>
        /// Gets the number of bytes per color channel per image row.
        /// </summary>
        private uint SizePerUnalignedRow { get; }

        /// <summary>
        /// Gets the stride per color channel per image row.
        /// The stride is the "number of zero bytes" that are needed to align an image row at 4 or 8 byte boundaries.
        /// </summary>
        public byte Stride { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Converts a <see cref="ImageMemory"/> to a byte array.
        /// </summary>
        public byte[] AsArray()
        {
            return Data;
        }

        /// <summary>
        /// Creates a human readable string representation of this instance.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>
        public override string ToString()
        {
            var length = Data.LongLength;
            var unit = "byte";
            if (Data.LongLength > 1024 && Data.LongLength < 1024 * 1024)
            {
                length /= 1024;
                unit = "kb";
            }
            else if (Data.LongLength > 1024 * 1024 && Data.LongLength < 1024 * 1024 * 1024)
            {
                length /= (1024 * 1024);
                unit = "mb";
            }
            else if (Data.LongLength > 1024 * 1024 * 1024)
            {
                length /= (1024 * 1024 * 1024);
                unit = "gb";
            }

            return $"Alignment: {Alignment}, Color Channels: {ColorChannels}, Precision: {Precision}, Size: {length}{unit}";
        }

        #endregion
    }
}