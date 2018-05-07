namespace CustomCode.Data.Imaging.Bmp
{
    using System.IO;

    /// <summary>
    /// Implementation of the BITMAPFILEHEADER structure.
    /// </summary>
    /// <remarks>
    /// See <a href="https://msdn.microsoft.com/en-us/library/dd183374.aspx"/> for details.
    /// </remarks>
    public sealed class FileHeader
    {
        #region Data

        /// <summary>
        /// Gets the type of the bitmap file (should always be "BM").
        /// </summary>
        public ushort Type { get; private set; }

        /// <summary>
        /// Gets the size of the bitmap file in bytes.
        /// </summary>
        public uint Size { get; private set; }

        /// <summary>
        /// Gets the offset to the start of the pixel data.
        /// </summary>
        public uint DataOffset { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// Parse the header data from bitmap <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader"> A binary reader that represents the bitmap file. </param>
        public void Parse(BinaryReader reader)
        {
            Type = reader.ReadUInt16();
            Size = reader.ReadUInt32();
            reader.BaseStream.Position += 4; // skipt 4 reserved bytes
            DataOffset = reader.ReadUInt32();
        }

        #endregion
    }
}