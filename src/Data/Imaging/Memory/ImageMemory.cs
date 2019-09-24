namespace CustomCode.Data.Imaging.Memory
{
    using System;

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
            (uint width, uint height) dimension,
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
                SizePerPixel = dimension.width;
                SizePerUnalignedRow = (uint)(dimension.width / 8.0);
                if (dimension.width % 8 != 0)
                {
                    ++SizePerUnalignedRow;
                }
            }
            else
            {
                SizePerUnalignedRow = dimension.width * (uint)Precision;
            }

            Stride = 0;
            var inverseStride = SizePerUnalignedRow % (uint)Alignment;
            if (inverseStride > 0)
            {
                Stride = (byte)((uint)Alignment - inverseStride);
            }

            SizePerAlignedRow = SizePerUnalignedRow + Stride;
            SizePerChannel = SizePerAlignedRow * dimension.height;

            var dataCount = SizePerChannel;
            if (ColorChannels != ColorChannels.Monochrome)
            {
                dataCount = (uint)ColorChannels * SizePerChannel;
            }
            Data = new byte[dataCount];
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public MemoryAlignment Alignment { get; }

        /// <inheritdoc />
        public ColorChannels ColorChannels { get; }

        /// <summary>
        /// Get the memory's raw data as byte array.
        /// </summary>
        private byte[] Data { get; }

        /// <inheritdoc />
        public MemoryPrecision Precision { get; }

        /// <inheritdoc />
        public uint Size
        {
            get { return (uint)Data.Length; }
        }

        /// <inheritdoc />
        public uint SizePerAlignedRow { get; }

        /// <inheritdoc />
        public uint SizePerChannel { get; }

        /// <inheritdoc />
        public uint SizePerPixel { get; }

        /// <summary>
        /// Gets the number of bytes per color channel per image row.
        /// </summary>
        private uint SizePerUnalignedRow { get; }

        /// <inheritdoc />
        public byte Stride { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public byte[] AsArray()
        {
            return Data;
        }

        /// <inheritdoc />
        public Memory<byte> AsMemory()
        {
            return new Memory<byte>(Data);
        }

        /// <inheritdoc />
        public ReadOnlyMemory<byte> AsReadOnlyMemory()
        {
            return new ReadOnlyMemory<byte>(Data);
        }

        /// <inheritdoc />
        public ReadOnlySpan<byte> AsReadOnlySpan()
        {
            return new ReadOnlySpan<byte>(Data);
        }

        /// <inheritdoc />
        public Span<byte> AsSpan()
        {
            return new Span<byte>(Data);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var length = Data.Length;
            var unit = "byte";
            if (Data.Length > 1024 && Data.Length < 1024 * 1024)
            {
                length /= 1024;
                unit = "kb";
            }
            else if (Data.Length > 1024 * 1024 && Data.Length < 1024 * 1024 * 1024)
            {
                length /= 1024 * 1024;
                unit = "mb";
            }
            else if (Data.Length > 1024 * 1024 * 1024)
            {
                length /= 1024 * 1024 * 1024;
                unit = "gb";
            }

            return $"Alignment: {Alignment}, Color Channels: {ColorChannels}, Precision: {Precision}, Size: {length}{unit}";
        }

        #endregion
    }
}