namespace CustomCode.Data.Imaging.Memory.Bmp
{
    using Imaging.Bmp;
    using Memory;
    using System;
    using System.IO;

    /// <summary>
    /// A <see cref="IMemoryParser"/> for 4bit bitmaps where bits are interpreted as rgb color table values.
    /// </summary>
    public sealed class FourBitRgbParser : IMemoryParser
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FourBitRgbParser"/> type.
        /// </summary>
        /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="colorTable"> The bitmap's color table. </param>
        /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
        public FourBitRgbParser(MemoryAlignment alignment, IColorTable colorTable, int height, uint width)
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
            var memory = new ImageMemory((Width, (uint)Math.Abs(Height)), Alignment, ColorChannels.Rgb, MemoryPrecision.EightBit);
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
                    ParseRgbRow(reader, h, ref data, padding, memory.SizePerAlignedRow, memory.SizePerChannel);
                }
            }
            else // rows are stored top down
            {
                var absHeight = (Height * -1);
                for (var h = 0; h < absHeight; ++h)
                {
                    ParseRgbRow(reader, h, ref data, padding, memory.SizePerAlignedRow, memory.SizePerChannel);
                }
            }

            return memory;
        }

        /// <summary>
        /// Parse a single rgb bitmap pixel row.
        /// </summary>
        /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
        /// <param name="rowIndex"> The index of the row to be parsed. </param>
        /// <param name="data"> The parsed row data. </param>
        /// <param name="sizePerAlignedRow"> The number of bytes per aligned image memory row. </param>
        /// <param name="sizePerChannel"> The number of bytes per color channel. </param>
        /// <param name="padding"> The number of padding bytes per image memory row. </param>
        private void ParseRgbRow(BinaryReader reader, int rowIndex, ref byte[] data,
            uint padding, uint sizePerAlignedRow, uint sizePerChannel)
        {
            var isUneven = (Width % 2 != 0);
            var offsetRed = rowIndex * sizePerAlignedRow;
            var offsetGreen = offsetRed + sizePerChannel;
            var offsetBlue = offsetGreen + sizePerChannel;

            for (var w = 0u; w < Width - 2; w += 2)
            {
                var indices = reader.ReadByte();
                var firstIndex = indices >> 4;
                var secondIndex = indices & 0x0F;

                var color = ColorTable[firstIndex];
                data[offsetRed + w] = color.red;
                data[offsetGreen + w] = color.green;
                data[offsetBlue + w] = color.blue;

                color = ColorTable[secondIndex];
                data[offsetRed + w + 1] = color.red;
                data[offsetGreen + w + 1] = color.green;
                data[offsetBlue + w + 1] = color.blue;
            }

            if (isUneven)
            {
                var indices = reader.ReadByte();
                var firstIndex = indices >> 4;
                var offset = Width - 1;

                var (red, green, blue) = ColorTable[firstIndex];
                data[offsetRed + offset] = red;
                data[offsetGreen + offset] = green;
                data[offsetBlue + offset] = blue;
            }

            reader.BaseStream.Position += padding;
        }

        #endregion
    }
}