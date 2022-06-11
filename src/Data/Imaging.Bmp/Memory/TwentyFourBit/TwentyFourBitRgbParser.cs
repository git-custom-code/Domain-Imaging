namespace CustomCode.Data.Imaging.Memory.Bmp;

using Memory;
using System;
using System.IO;

/// <summary>
/// A <see cref="IMemoryParser"/> for 24bit bitmaps where bits are interpreted as rgb color table values.
/// </summary>
public sealed class TwentyFourBitRgbParser : IMemoryParser
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="TwentyFourBitRgbParser"/> type.
    /// </summary>
    /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
    public TwentyFourBitRgbParser(MemoryAlignment alignment, int height, uint width)
    {
        Alignment = alignment;
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
        var padding = 4 - ((Width * 3) % 4);

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
            var absHeight = -1 * Height;
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
        var rowRed = rowIndex * sizePerAlignedRow;
        var rowGreen = rowRed + sizePerChannel;
        var rowBlue = rowGreen + sizePerChannel;

        for (var w = 0; w < Width; ++w)
        {
            data[rowBlue + w] = reader.ReadByte();
            data[rowGreen + w] = reader.ReadByte();
            data[rowRed + w] = reader.ReadByte();
        }
        
        reader.BaseStream.Position += padding;
    }

    #endregion
}
