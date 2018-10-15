namespace CustomCode.Data.Imaging.Memory.Bmp
{
    using Imaging.Bmp;
    using Memory;
    using System;
    using System.IO;

    /// <summary>
    /// A <see cref="IMemoryParser"/> for 4bit bitmaps where bits are interpreted as gray scale color table values.
    /// </summary>
    public sealed class FourBitGrayScaleParser : IMemoryParser
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FourBitGrayScaleParser"/> type.
        /// </summary>
        /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="colorTable"> The bitmap's color table. </param>
        /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
        public FourBitGrayScaleParser(MemoryAlignment alignment, IColorTable colorTable, int height, uint width)
        {
            Alignment = alignment;
            ColorTable = colorTable ?? throw new ArgumentNullException(nameof(colorTable));
            Height = height;
            Width = width;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the alignment of the parsed <see cref="IImageMemory"/>.
        /// </summary>
        private MemoryAlignment Alignment { get; }

        /// <summary>
        /// Gets the bitmap's color table.
        /// </summary>
        private IColorTable ColorTable { get; }

        /// <summary>
        /// Gets the number of pixels in y-direction of the parsed <see cref="IImageMemory"/>.
        /// </summary>
        private int Height { get; }

        /// <summary>
        /// Gets the number of pixels in x-direction of the parsed <see cref="IImageMemory"/>.
        /// </summary>
        private uint Width { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public IImageMemory Parse(BinaryReader reader)
        {
            var memory = new ImageMemory((Width, (uint)Math.Abs(Height)), Alignment, ColorChannels.Gray, MemoryPrecision.EightBit);
            var bytesPerRow = ((Width + 1u) / 2u);
            var padding = bytesPerRow % 4;
            if (padding > 0u)
            {
                padding = 4u - bytesPerRow;
            }

            var data = memory.AsArray();
            if (Height > 0) // rows are stored bottom up
            {
                for (var h = Height - 1; h >= 0; --h)
                {
                    ParseGrayScaleRow(reader, h, ref data, memory.SizePerAlignedRow, padding);
                }
            }
            else // rows are stored top down
            {
                var absHeight = (Height * -1);
                for (var h = 0; h < absHeight; ++h)
                {
                    ParseGrayScaleRow(reader, h, ref data, memory.SizePerAlignedRow, padding);
                }
            }

            return memory;
        }

        /// <summary>
        /// Parse a single gray scale bitmap pixel row.
        /// </summary>
        /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
        /// <param name="rowIndex"> The index of the row to be parsed. </param>
        /// <param name="data"> The parsed row data. </param>
        /// <param name="sizePerAlignedRow"> The number of bytes per aligned image memory row. </param>
        /// <param name="padding"> The number of padding bytes per image memory row. </param>
        private void ParseGrayScaleRow(BinaryReader reader, int rowIndex, ref byte[] data,
            uint padding, uint sizePerAlignedRow)
        {
            var isUneven = (Width % 2 != 0);
            var offset = rowIndex * sizePerAlignedRow;
            for (var w = 0u; w < Width - 2; w += 2)
            {
                var indices = reader.ReadByte();
                var firstIndex = indices & 0x0F;
                var secondIndex = indices >> 4;

                data[offset + w] = ColorTable[firstIndex].red; // red == green == blue
                data[offset + w + 1] = ColorTable[secondIndex].red; // red == green == blue
            }

            if (isUneven)
            {
                var indices = reader.ReadByte();
                var firstIndex = indices >> 4;

                data[offset + Width - 1] = ColorTable[firstIndex].red; // red == green == blue
            }

            reader.BaseStream.Position += padding;
        }

        #endregion
    }
}