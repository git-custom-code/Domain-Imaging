namespace CustomCode.Data.Imaging.Bmp
{
    using System;
    using System.IO;

    /// <summary>
    /// Implementation of the BITMAPINFOHEADER structure.
    /// </summary>
    /// <remarks>
    /// See <a href="https://msdn.microsoft.com/en-us/library/dd183376.aspx"/> for details.
    /// </remarks>
    public class InfoHeader : CoreHeader
    {
        #region Data

        /// <summary>
        /// The size of the header in bytes.
        /// </summary>
        public new const uint Size = 40;

        /// <summary>
        /// Gets the type of compression for a compressed bottom-up bitmap.
        /// </summary>
        /// <remarks>
        /// Top-down bitmaps cannot be compressed.
        /// </remarks>
        public CompressionType Compression { get; private set; }

        /// <summary>
        /// Gets the size, in bytes, of the image. This may be set to zero for uncompressed bitmaps.
        /// </summary>
        public uint SizeOfBitmap { get; private set; }

        /// <summary>
        /// Gets the horizontal resolution, in pixels-per-meter, of the target device for the bitmap.
        /// </summary>
        public int HorizontalResolution { get; private set; }

        /// <summary>
        /// Gets the veritcal resolution, in pixels-per-meter, of the target device for the bitmap.
        /// </summary>
        public int VerticalResolution { get; private set; }

        /// <summary>
        /// Gets the number of color indexes in the color table that are actually used by the bitmap
        /// </summary>
        /// <remarks></remarks>
        /// If this value is zero, the bitmap uses the maximum number of colors corresponding to the value of the
        /// <see cref="CoreHeader.BitsPerPixel"/> member for the compression mode specified by <see cref="Compression"/>
        ///
        /// If <see cref="ColorsUsed"/> is nonzero and the <see cref="CoreHeader.BitsPerPixel"/> member is less than 16,
        /// the <see cref="ColorsUsed"/> member specifies the actual number of colors the graphics engine
        /// or device driver accesses.
        ///
        /// If <see cref="CoreHeader.BitsPerPixel"/> is 16 or greater, the <see cref="ColorsUsed"/> member specifies the size
        /// of the color table used to optimize performance of the system color palettes.
        ///
        /// If <see cref="CoreHeader.BitsPerPixel"/> equals 16 or 32, the optimal color palette starts immediately
        /// following the three DWORD masks.
        ///
        /// When the bitmap array immediately follows the BITMAPINFO structure, it is a packed bitmap.
        /// Packed bitmaps are referenced by a single pointer. Packed bitmaps require that the <see cref="ColorsUsed"/>
        /// member must be either zero or the actual size of the color table.
        /// </remarks>
        public uint ColorsUsed { get; private set; }

        /// <summary>
        /// Gets the number of color indexes that are required for displaying the bitmap.
        /// If this value is zero, all colors are required.
        /// </summary>
        public uint ColorsImportant { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// Parse the header data from bitmap <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader"> A binary reader that represents the bitmap file. </param>
        public override void Parse(BinaryReader reader)
        {
            Width = reader.ReadInt32();
            Height = reader.ReadInt32();
            ColorPlaneCount = reader.ReadUInt16();
            BitsPerPixel = reader.ReadUInt16();
            Compression = (CompressionType)reader.ReadUInt32();
            SizeOfBitmap = reader.ReadUInt32();
            HorizontalResolution = reader.ReadInt32();
            VerticalResolution = reader.ReadInt32();
            ColorsUsed = reader.ReadUInt32();
            if (ColorsUsed == 0 && BitsPerPixel < 16)
            {
                ColorsUsed = (uint)Math.Pow(2, BitsPerPixel);
            }
            ColorsImportant = reader.ReadUInt32();
        }

        #endregion
    }
}