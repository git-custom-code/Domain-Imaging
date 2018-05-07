namespace CustomCode.Data.Imaging.Bmp
{
    using Core.Composition;
    using Memory;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation for a bitmap image file parser.
    /// </summary>
    [Export(typeof(IImageFileParser))]
    public sealed class BitmapParser : IImageFileParser
    {
        #region Logic

        /// <summary>
        /// Query if the parser can read bitmao image files with the specified <paramref name="fileExtension"/>.
        /// </summary>
        /// <param name="fileExtension"> The bitmap image's file extension. </param>
        /// <returns> True if the parser can read the specified bitmap <paramref name="fileExtension"/>, false otherwise. </returns>
        public bool CanParse(string fileExtension)
        {
            return "bmp".Equals(fileExtension, StringComparison.OrdinalIgnoreCase) ||
                ".bmp".Equals(fileExtension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Parse an image file from a binary stream. 
        /// </summary>
        /// <param name="reader"> The reader to the binary image file stream. </param>
        /// <param name="alignment"> The desired memory alignment of the parsed <see cref="IImageMemory"/>. </param>
        /// <returns> The parsed <see cref="IImageMemory"/>. </returns>
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
                if (infoHeader.ColorsUsed > 0)
                {

                }

                reader.BaseStream.Position = fileHeader.DataOffset;

                if (infoHeader.BitsPerPixel == 24)
                {
                    var result = new ImageMemory(
                        ((uint)infoHeader.Width, (uint)infoHeader.Height),
                        alignment,
                        ColorChannels.Rgb,
                        MemoryPrecision.EightBit);

                    if (infoHeader.ColorsUsed == 0)
                    {
                        Parse24bitData(reader, infoHeader.Width, infoHeader.Height, ref result);
                    }
                    else
                    {
                        Parse24bitData(reader, infoHeader.Width, infoHeader.Height, ref result);
                    }

                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Parse 24bit bgr color data.
        /// </summary>
        /// <param name="reader"> The reader to the binary image file stream. </param>
        /// <param name="width"> The number of bgr pixels per image row. </param>
        /// <param name="height"> THe number of rows per image. </param>
        /// <param name="memory"> The parsed <see cref="IImageMemory"/>. </param>
        private void Parse24bitData(BinaryReader reader, int width, int height, ref ImageMemory memory)
        {
            var channelGreen = memory.SizePerChannel;
            var channelBlue = channelGreen + memory.SizePerChannel;
            var padding = (4 - ((width * 3) % 4));
            var data = memory.AsArray();

            if (height > 0) // rows are stored bottom up
            {
                for (var h = height - 1; h >= 0; --h)
                {
                    var rowRed = h * memory.SizePerAlignedRow;
                    var rowGreen = channelGreen + h * memory.SizePerAlignedRow;
                    var rowBlue = channelBlue + h * memory.SizePerAlignedRow;

                    for (var w = 0; w < width; ++w)
                    {
                        data[rowBlue + w] = reader.ReadByte();
                        data[rowGreen + w] = reader.ReadByte();
                        data[rowRed + w] = reader.ReadByte();
                    }

                    reader.BaseStream.Position += padding;
                }
            }
            else // rows are stored top down
            {
                var absHeight = height * -1;
                for (var h = 0; h < absHeight; ++h)
                {
                    var rowRed = h * memory.SizePerAlignedRow;
                    var rowGreen = channelGreen + h * memory.SizePerAlignedRow;
                    var rowBlue = channelBlue + h * memory.SizePerAlignedRow;

                    for (var w = 0; w < width; ++w)
                    {
                        data[rowBlue + w] = reader.ReadByte(); // blue
                        data[rowGreen + w] = reader.ReadByte(); // green
                        data[rowRed + w] = reader.ReadByte(); // red
                    }

                    reader.BaseStream.Position += padding;
                }
            }
        }

        /// <summary>
        /// Asynchronously parse an image file from a binary stream. 
        /// </summary>
        /// <param name="reader"> The reader to the binary image file stream. </param>
        /// <param name="alignment"> The desired memory alignment of the parsed <see cref="IImageMemory"/>. </param>
        /// <returns> An awaitable task with the parsed <see cref="IImageMemory"/>. </returns>
        public Task<IImageMemory> ParseAsync(BinaryReader reader, MemoryAlignment alignment)
        {
            return Task.Run(() => Parse(reader, alignment));
        }

        #endregion
    }
}