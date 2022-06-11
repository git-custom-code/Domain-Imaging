namespace CustomCode.Data.Imaging.Bmp;

using Core.Composition;
using Memory;
using Memory.Bmp;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Implementation of a bitmap image file parser.
/// </summary>
[Export(typeof(IImageFileParser))]
public sealed class BitmapParser : IImageFileParser
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="BitmapParser"/> type.
    /// </summary>
    /// <param name="parserFactory"> A factory that can be used to create <see cref="IMemoryParser"/> instances. </param>
    public BitmapParser(IMemoryParserFactory parserFactory)
    {
        ParserFactory = parserFactory ?? throw new ArgumentNullException(nameof(parserFactory));
    }

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMemoryParser"/> instances.
    /// </summary>
    private IMemoryParserFactory ParserFactory { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IImageFileParser" />
    public bool CanParse(string fileExtension)
    {
        return "bmp".Equals(fileExtension, StringComparison.OrdinalIgnoreCase) ||
            ".bmp".Equals(fileExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc cref="IImageFileParser" />
    public IImageMemory Parse(BinaryReader reader, MemoryAlignment alignment)
    {
        var fileHeader = new FileHeader();
        fileHeader.Parse(reader);
        var infoHeaderSize = reader.ReadUInt32();
        if (infoHeaderSize == CoreHeader.Size) // bmp version 2
        {
            var coreHeader = new CoreHeader();
            coreHeader.Parse(reader);
        }
        else if (infoHeaderSize == InfoHeader.Size) // bmp version 3
        {
            var infoHeader = new InfoHeader();
            infoHeader.Parse(reader);

            ColorTable colorTable = null;
            if (infoHeader.ColorsUsed > 0)
            {
                colorTable = ParseColorTable(reader, infoHeader.ColorsUsed, true);
            }
            reader.BaseStream.Position = fileHeader.DataOffset;

            var parser = ParserFactory.Create(alignment, colorTable, infoHeader);
            var memory = parser.Parse(reader);
            return memory;
        }

        return null;
    }

    /// <inheritdoc cref="IImageFileParser" />
    public Task<IImageMemory> ParseAsync(BinaryReader reader, MemoryAlignment alignment, CancellationToken? token = null)
    {
        return Task.Run(() => Parse(reader, alignment), token ?? CancellationToken.None);
    }

    /// <summary>
    /// Parse the bitmap's color table. 
    /// </summary>
    /// <param name="reader"> The reader to the binary image file stream. </param>
    /// <param name="colorsUsed"> The number of color's stored in the color table. </param>
    /// <param name="hasPadding"> A flag indicating whether or not the color table entries are padded at 4 byte boundaries. </param>
    /// <returns> The parsed color table. </returns>
    private ColorTable ParseColorTable(BinaryReader reader, uint colorsUsed, bool hasPadding = false)
    {
        var colorTable = new ColorTable();

        if (hasPadding)
        {
            for (var i = 0; i < colorsUsed; ++i)
            {
                var blue = reader.ReadByte();
                var green = reader.ReadByte();
                var red = reader.ReadByte();
                reader.BaseStream.Position++;

                colorTable.Add((red, green, blue));
            }
        }
        else
        {
            for (var i = 0; i < colorsUsed; ++i)
            {
                var blue = reader.ReadByte();
                var green = reader.ReadByte();
                var red = reader.ReadByte();

                colorTable.Add((red, green, blue));
            }
        }

        return colorTable;
    }

    #endregion
}
