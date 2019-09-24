namespace CustomCode.Data.Imaging.Memory.Bmp
{
    using Imaging.Bmp;
    using Memory;
    using System;
    using System.IO;

    /// <summary>
    /// A <see cref="IMemoryParser"/> for 4bit bitmaps where bits are interpreted as rgb color table values.
    /// </summary>
    public sealed class FourBitRgbRleParser : IMemoryParser
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FourBitRgbRleParser"/> type.
        /// </summary>
        /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="colorTable"> The bitmap's color table. </param>
        /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
        public FourBitRgbRleParser(MemoryAlignment alignment, IColorTable colorTable, int height, uint width)
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
            ParseRleEncodedRow(reader, 0, ref data, padding, memory.SizePerAlignedRow, memory.SizePerChannel);
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
        private void ParseRleEncodedRow(BinaryReader reader, int rowIndex, ref byte[] data,
            uint padding, uint sizePerAlignedRow, uint sizePerChannel)
        {
            var isUneven = (Width % 2 != 0);
            var offsetRed = rowIndex * sizePerAlignedRow;
            var offsetGreen = offsetRed + sizePerChannel;
            var offsetBlue = offsetGreen + sizePerChannel;
            var offset = 0;

            while (true)
            {
                var pixelCount = reader.ReadByte();
                if (pixelCount == 0)
                {
                    pixelCount = reader.ReadByte();
                    var count = (((uint)pixelCount + 1u) / 2u);
                    padding = count % 4;
                    if (padding > 0u)
                    {
                        padding = 4u - count;
                    }

                    if (pixelCount == 0)
                    {

                    }
                    else if (pixelCount == 1)
                    {

                    }
                    else if (pixelCount == 2)
                    {

                    }
                    else
                    {
                        for (var i = 0u; i < count; ++i)
                        {
                            var indices = reader.ReadByte();
                            var firstIndex = indices >> 4;
                            var secondIndex = indices & 0x0F;

                            var (red, green, blue) = ColorTable[firstIndex];
                            data[offsetRed + offset] = red;
                            data[offsetGreen + offset] = green;
                            data[offsetBlue + offset] = blue;
                            ++offset;

                            (red, green, blue) = ColorTable[secondIndex];
                            data[offsetRed + offset] = red;
                            data[offsetGreen + offset] = green;
                            data[offsetBlue + offset] = blue;
                            ++offset;
                        }

                        reader.BaseStream.Position += padding;
                    }
                }
                else
                {
                    var indices = reader.ReadByte();
                    var firstIndex = indices >> 4;
                    var secondIndex = indices & 0x0F;
                    
                    var (red, green, blue) = ColorTable[firstIndex];
                    var (red2, green2, blue2) = ColorTable[secondIndex];

                    for (var i = 0; i < (int)pixelCount - 1; i += 2)
                    {                        
                        data[offsetRed + offset] = red;
                        data[offsetGreen + offset] = green;
                        data[offsetBlue + offset] = blue;
                        ++offset;

                        data[offsetRed + offset] = red2;
                        data[offsetGreen + offset] = green2;
                        data[offsetBlue + offset] = blue2;
                        ++offset;
                    }

                    if (pixelCount % 2 > 0)
                    {
                        data[offsetRed + offset] = red;
                        data[offsetGreen + offset] = green;
                        data[offsetBlue + offset] = blue;
                        ++offset;
                    }
                }
            }

            reader.BaseStream.Position += padding;
        }

        #endregion
    }
}