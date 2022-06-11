namespace CustomCode.Data.Imaging.Memory.Bmp;

using Imaging.Bmp;
using Memory;
using System;
using System.IO;

/// <summary>
/// A <see cref="IMemoryParser"/> for 8bit bitmaps where bits are interpreted as gray scale color table values.
/// </summary>
public sealed class EightBitGrayScaleParser : IMemoryParser
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="EightBitGrayScaleParser"/> type.
    /// </summary>
    /// <param name="alignment"> The alignment of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="colorTable"> The bitmap's color table. </param>
    /// <param name="height"> The number of pixels in y-direction of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="width"> The number of pixels in x-direction of the parsed <see cref="IImageMemory"/>. </param>
    public EightBitGrayScaleParser(MemoryAlignment alignment, IColorTable colorTable, int height, uint width)
    {
        Alignment = alignment;
        ColorTable = colorTable;
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
        var memory = new ImageMemory((Width, (uint)Math.Abs(Height)), Alignment, ColorChannels.Gray, MemoryPrecision.EightBit);
        var padding = 4 - (Width % 4);

        var data = memory.AsArray();
        if (Height > 0) // rows are stored bottom up
        {
            if (ColorTable == null)
            {
                for (var h = Height - 1; h >= 0; --h)
                {
                    ParseGrayScaleRow(reader, h, ref data, padding, memory.SizePerAlignedRow);
                }
            }
            else
            {
                for (var h = Height - 1; h >= 0; --h)
                {
                    ParseGrayScalePaletteRow(reader, h, ref data, padding, memory.SizePerAlignedRow);
                }
            }
        }
        else // rows are stored top down
        {
            var absHeight = -1 * Height;

            if (ColorTable == null)
            {
                for (var h = 0; h < absHeight; ++h)
                {
                    ParseGrayScaleRow(reader, h, ref data, padding, memory.SizePerAlignedRow);
                }
            }
            else
            {
                for (var h = 0; h < absHeight; ++h)
                {
                    ParseGrayScalePaletteRow(reader, h, ref data, padding, memory.SizePerAlignedRow);
                }
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
        var row = rowIndex * sizePerAlignedRow;
        
        for (var w = 0; w < Width; ++w)
        {
            data[row + w] = reader.ReadByte();
        }

        reader.BaseStream.Position += padding;
    }

    /// <summary>
    /// Parse a single gray scale bitmap pixel row using the color table.
    /// </summary>
    /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
    /// <param name="rowIndex"> The index of the row to be parsed. </param>
    /// <param name="data"> The parsed row data. </param>
    /// <param name="sizePerAlignedRow"> The number of bytes per aligned image memory row. </param>
    /// <param name="padding"> The number of padding bytes per image memory row. </param>
    private void ParseGrayScalePaletteRow(BinaryReader reader, int rowIndex, ref byte[] data,
        uint padding, uint sizePerAlignedRow)
    {
        var row = rowIndex * sizePerAlignedRow;

        for (var w = 0; w < Width; ++w)
        {
            var index = reader.ReadByte();
            var (red, green, blue) = ColorTable[index];
            data[row + w] = blue; // since the color table is gray scale, blue == green == red
        }

        reader.BaseStream.Position += padding;
    }

    #endregion
}
