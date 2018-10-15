namespace CustomCode.Data.Imaging.Memory.Bmp
{
    using Imaging.Bmp;
    using Memory;
    using System;
    using System.IO;

    /// <summary>
    /// A <see cref="IMemoryParser"/> for 1bit bitmaps where bits are interpreted as gray scale color table values.
    /// </summary>
    public sealed class OneBitGrayScaleParser : IMemoryParser
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="OneBitGrayScaleParser"/> type.
        /// </summary>
        /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="colorTable"> The bitmap's color table. </param>
        /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
        /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
        public OneBitGrayScaleParser(MemoryAlignment alignment, IColorTable colorTable, int height, uint width)
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
            var bytesPerRow = ((Width + 7u) / 8u);
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
                    var offset = h * memory.SizePerAlignedRow;
                    for (var w = 0u; w < bytesPerRow; ++w)
                    {
                        var bits = reader.ReadByte();
                        for (var b = 7; b >= 0; --b)
                        {
                            if ((bits & (1 << b)) != 0)
                            {
                                data[offset + w] = ColorTable[1].red; // red == green == blue
                            }
                            else
                            {
                                data[offset + w] = ColorTable[0].red; // red == green == blue
                            }
                        }
                    }
                    reader.BaseStream.Position += padding;
                }
            }
            else // rows are stored top down
            {
                var absHeight = (uint)(Height * -1);
                for (var h = 0u; h < absHeight; ++h)
                {
                    var offset = h * memory.SizePerAlignedRow;
                    for (var w = 0u; w < Width; ++w)
                    {
                        var bits = reader.ReadByte();
                        for (var b = 7; b >= 0; --b)
                        {
                            if ((bits & (1 << b)) != 0)
                            {
                                data[offset + w] = ColorTable[1].red; // red == green == blue
                            }
                            else
                            {
                                data[offset + w] = ColorTable[0].red; // red == green == blue
                            }
                        }
                    }
                    reader.BaseStream.Position += padding;
                }
            }

            return memory;
        }

        #endregion
    }
}