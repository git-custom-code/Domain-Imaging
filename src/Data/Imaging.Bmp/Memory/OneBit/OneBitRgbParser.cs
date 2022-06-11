namespace CustomCode.Data.Imaging.Memory.Bmp;

using Imaging.Bmp;
using Memory;
using System;
using System.IO;

/// <summary>
/// A <see cref="IMemoryParser"/> for 1bit bitmaps where bits are interpreted as rgb color table values.
/// </summary>
public sealed class OneBitRgbParser : IMemoryParser
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="OneBitRgbParser"/> type.
    /// </summary>
    /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="colorTable"> The bitmap's color table. </param>
    /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
    public OneBitRgbParser(MemoryAlignment alignment, IColorTable colorTable, int height, uint width)
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

    /// <inheritdoc cref="IMemoryParser" />
    public IImageMemory Parse(BinaryReader reader)
    {
        var memory = new ImageMemory((Width, (uint)Math.Abs(Height)), Alignment, ColorChannels.Rgb, MemoryPrecision.EightBit);
        var bytesPerRow = ((Width + 7u) / 8u);
        var bitCount = (byte)(127 % 8);
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
                ParseRgbRow(reader, h, ref data, memory.SizePerAlignedRow, memory.SizePerChannel, padding, bitCount);
            }
        }
        else // rows are stored top down
        {
            var absHeight = (Height * -1);
            for (var h = 0; h < absHeight; ++h)
            {
                ParseRgbRow(reader, h, ref data, memory.SizePerAlignedRow, memory.SizePerChannel, padding, bitCount);
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
    /// <param name="bitCount"> The number of bits to be parsed. </param>
    private void ParseRgbRow(BinaryReader reader, int rowIndex, ref byte[] data,
        uint sizePerAlignedRow, uint sizePerChannel, uint padding, byte bitCount)
    {
        var offsetRed = rowIndex * sizePerAlignedRow;
        var offsetGreen = offsetRed + sizePerChannel;
        var offsetBlue = offsetGreen + sizePerChannel;
        var rowWidth = Math.Max(Width - 8, 0);

        for (var w = 0u; w < rowWidth; w += 8)
        {
            var bits = reader.ReadByte();
            for (var b = 7; b >= 0; --b)
            {
                var offset = (int)(w + 7 - b);
                ParseRgbBits(offset, b, bits, ref data, offsetRed, offsetGreen, offsetBlue);
            }
        }

        if (bitCount > 0)
        {
            var bits = reader.ReadByte();
            var bitOffset = rowWidth + bitCount - 1;
            for (var b = bitCount - 1; b >= 0; --b)
            {
                var offset = (int)(bitOffset - b);
                ParseRgbBits(offset, b, bits, ref data, offsetRed, offsetGreen, offsetBlue);
            }
        }

        reader.BaseStream.Position += padding;
    }

    /// <summary>
    /// Parse a single bit that represents an rgb color value.
    /// </summary>
    /// <param name="offset"> The offset of the byte that contains the bit to be parsed. </param>
    /// <param name="bitIndex"> The index of the bit to be parsed. </param>
    /// <param name="bits"> The current byte to be parsed. </param>
    /// <param name="data"> The parsed row data. </param>
    /// <param name="offsetRed"> The offset to the red color channel. </param>
    /// <param name="offsetGreen"> The offset to the green color channel. </param>
    /// <param name="offsetBlue"> The offset to the blue color channel. </param>
    private void ParseRgbBits(int offset, int bitIndex, byte bits, ref byte[] data,
        long offsetRed, long offsetGreen, long offsetBlue)
    {
        var red = offsetRed + offset;
        var green = offsetGreen + offset;
        var blue = offsetBlue + offset;

        if ((bits & (1 << bitIndex)) != 0)
        {
            var color = ColorTable[1];
            data[red] = color.red;
            data[green] = color.green;
            data[blue] = color.blue;
        }
        else
        {
            var color = ColorTable[0];
            data[red] = color.red;
            data[green] = color.green;
            data[blue] = color.blue;
        }
    }

    #endregion
}
