namespace CustomCode.Data.Imaging.Memory.Bmp;

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

    /// <summary> Rle marker that signals the end of an image row. </summary>
    private const byte EndOfImageRow = 0;

    /// <summary> Rle marker that signals the end of the image. </summary>
    private const byte EndOfImage = 1;

    /// <summary> Rle marker that signals a delta code (as offset to the next pixel). </summary>
    private const byte DeltaCode = 2;

    #endregion

    #region Logic

    /// <inheritdoc cref="IMemoryParser" />
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
                ParseRleEncodedRow(reader, h, ref data, padding, memory.SizePerAlignedRow, memory.SizePerChannel);
            }
        }
        else // rows are stored top down
        {
            var absHeight = (Height * -1);
            for (var h = 0; h < absHeight; ++h)
            {
                ParseRleEncodedRow(reader, h, ref data, padding, memory.SizePerAlignedRow, memory.SizePerChannel);
            }
        }
        
        return memory;
    }

    /// <summary>
    /// Parse runtime-length encoded pixel data.
    /// </summary>
    /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
    /// <param name="rowIndex"> The index of the row to be parsed. </param>
    /// <param name="data"> The parsed row data. </param>
    /// <param name="padding"> The number of padding bytes per image memory row. </param>
    /// <param name="sizePerAlignedRow"> The number of bytes per aligned image memory row. </param>
    /// <param name="sizePerChannel"> The number of bytes per color channel. </param>
    private void ParseRleEncodedRow(BinaryReader reader, int rowIndex, ref byte[] data,
        uint padding, uint sizePerAlignedRow, uint sizePerChannel)
    {
        var offsetRed = (ulong)(rowIndex * sizePerAlignedRow);
        var offsetGreen = offsetRed + sizePerChannel;
        var offsetBlue = offsetGreen + sizePerChannel;
        var offset = 0ul;
        var imageLength = (ulong)reader.BaseStream.Length;

        while (offset < imageLength)
        {
            var pixelCount = reader.ReadByte();
            if (pixelCount == 0) // absolute mode (unencoded pixels or rle marker)
            {
                pixelCount = reader.ReadByte();
                if (pixelCount == EndOfImageRow)
                {
                    if (offset % Width != 0ul)
                    {
                        throw new Exception("Suspicious offset");
                    }
                    reader.BaseStream.Position += padding; // Use padding here?
                    return;
                }
                else if (pixelCount == EndOfImage)
                {
                    if (offset % Width != 0ul)
                    {
                        throw new Exception("Suspicious offset");
                    }
                    return;
                }
                else if (pixelCount == DeltaCode)
                {
                    var deltaX = reader.ReadByte();
                    var deltaY = reader.ReadByte();
                    offset += deltaX + deltaY * sizePerAlignedRow; // correct implementation?
                }
                else
                {
                    ParseAbsolutePixels(reader, ref data, pixelCount, offsetRed, offsetGreen, offsetBlue, ref offset);
                }
            }
            else // encoded mode (rle encoded pixels)
            {
                ParseEncodedPixels(reader, ref data, pixelCount, offsetRed, offsetGreen, offsetBlue, ref offset);
            }
        }
    }

    /// <summary>
    /// Parse the next <paramref name="pixelCount"/> unencoded pixels from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
    /// <param name="data"> The parsed pixel data. </param>
    /// <param name="pixelCount"> The number of pixels to be parsed. </param>
    /// <param name="offsetRed"> The offset to the first pixel of the red color channel. </param>
    /// <param name="offsetGreen"> The offset to the first pixel of the green color channel. </param>
    /// <param name="offsetBlue"> The offset to the first pixel of the blue color channel. </param>
    /// <param name="offset"> The offset to the current pixel to be read. </param>

    private void ParseAbsolutePixels(BinaryReader reader, ref byte[] data, byte pixelCount,
        ulong offsetRed, ulong offsetGreen, ulong offsetBlue, ref ulong offset)
    {
        for (var i = 0ul; i < (ulong)(pixelCount - 1); i += 2ul)
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

        if (pixelCount % 2 > 0)
        {
            var indices = reader.ReadByte();
            var firstIndex = indices >> 4;

            var (red, green, blue) = ColorTable[firstIndex];
            data[offsetRed + offset] = red;
            data[offsetGreen + offset] = green;
            data[offsetBlue + offset] = blue;
            ++offset;
        }

        var count = ((uint)pixelCount + 1u) / 2u;
        var padding = count % 2; // must be aligned at 2-byte boundary
        reader.BaseStream.Position += padding;
    }

    /// <summary>
    /// Parse the next <paramref name="pixelCount"/> runtime-length encoded pixels from the
    /// <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
    /// <param name="data"> The parsed pixel data. </param>
    /// <param name="pixelCount"> The number of pixels to be decoded. </param>
    /// <param name="offsetRed"> The offset to the first pixel of the current row in the red color channel. </param>
    /// <param name="offsetGreen"> The offset to the first pixel of the current row in the green color channel. </param>
    /// <param name="offsetBlue"> The offset to the first pixel of the current row in the blue color channel. </param>
    /// <param name="offset"> The offset to the current pixel to be read. </param>
    private void ParseEncodedPixels(BinaryReader reader, ref byte[] data, byte pixelCount,
        ulong offsetRed, ulong offsetGreen, ulong offsetBlue, ref ulong offset)
    {
        var indices = reader.ReadByte();
        var firstIndex = indices >> 4;
        var secondIndex = indices & 0x0F;

        var (firstRed, firstGreen, firstBlue) = ColorTable[firstIndex];
        var (secondRed, secondGreen, secondBlue) = ColorTable[secondIndex];

        for (var i = 0ul; i < (ulong)(pixelCount - 1); i += 2ul)
        {
            data[offsetRed + offset] = firstRed;
            data[offsetGreen + offset] = firstGreen;
            data[offsetBlue + offset] = firstBlue;
            ++offset;

            data[offsetRed + offset] = secondRed;
            data[offsetGreen + offset] = secondGreen;
            data[offsetBlue + offset] = secondBlue;
            ++offset;
        }

        if (pixelCount % 2 > 0)
        {
            data[offsetRed + offset] = firstRed;
            data[offsetGreen + offset] = firstGreen;
            data[offsetBlue + offset] = firstBlue;
            ++offset;
        }
    }

    #endregion
}
