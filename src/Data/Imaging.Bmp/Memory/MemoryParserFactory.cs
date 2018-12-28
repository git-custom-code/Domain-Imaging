namespace CustomCode.Data.Imaging.Memory.Bmp
{
    using Core.Composition;
    using Imaging.Bmp;
    using System;

    /// <summary>
    /// A factory that can be used to create an <see cref="IMemoryParser"/> that can be
    /// used to parse a bitmap's raw pixel data and returns a corresponding <see cref="IImageMemory"/>
    /// representation.
    /// </summary>
    [Export(typeof(IMemoryParserFactory))]
    public sealed class MemoryParserFactory : IMemoryParserFactory
    {
        #region Logic

        /// <inheritdoc />
        public IMemoryParser Create(MemoryAlignment alignment, IColorTable colorTable, InfoHeader header)
        {
            if (header.BitsPerPixel == 24)
            {
                if (colorTable != null)
                {
                    return new TwentyFourBitRgbPaletteParser(alignment, colorTable, header.Height, (uint)header.Width);
                }
                return new TwentyFourBitRgbParser(alignment, header.Height, (uint)header.Width);
            }
            else if (header.BitsPerPixel == 1)
            {
                if (colorTable.IsMonochrome())
                {
                    if (colorTable[0].red == 255)
                    {
                        return new OneBitWhiteBlackParser(alignment, header.Height, (uint)header.Width);
                    }
                    return new OneBitBlackWhiteParser(alignment, header.Height, (uint)header.Width);
                }
                else if (colorTable.IsGrayScale())
                {
                    return new OneBitGrayScaleParser(alignment, colorTable, header.Height, (uint)header.Width);
                }
                return new OneBitRgbParser(alignment, colorTable, header.Height, (uint)header.Width);
            }
            else if (header.BitsPerPixel == 4)
            {
                if (colorTable.IsGrayScale())
                {
                    return new FourBitGrayScaleParser(alignment, colorTable, header.Height, (uint)header.Width);
                }
                return new FourBitRgbParser(alignment, colorTable, header.Height, (uint)header.Width);
            }
            
            throw new NotSupportedException();
        }

        #endregion
    }
}