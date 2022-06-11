namespace CustomCode.Data.Imaging.Bmp;

using System.IO;

/// <summary>
/// Implementation of the BITMAPCOREHEADER structure.
/// </summary>
/// <remarks>
/// See <a href="https://msdn.microsoft.com/en-us/library/dd183372.aspx"/> for details.
/// </remarks>
public class CoreHeader
{
    #region Data

    /// <summary>
    /// The size of the header in bytes.
    /// </summary>
    public const uint Size = 12;

    /// <summary>
    /// Gets the image width in pixels.
    /// </summary>
    public int Width { get; protected set; }

    /// <summary>
    /// Gets the image height in pixels.
    /// </summary>
    public int Height { get; protected set; }

    /// <summary>
    /// Gets the number of color planes.
    /// </summary>
    public ushort ColorPlaneCount { get; protected set; }

    /// <summary>
    /// Gets the number of bits per pixel
    /// </summary>
    public ushort BitsPerPixel { get; protected set; }

    #endregion

    #region Logic

    /// <summary>
    /// Parse the header data from bitmap <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader"> A binary reader that represents the bitmap file. </param>
    public virtual void Parse(BinaryReader reader)
    {
        Width = reader.ReadUInt16();
        Height = reader.ReadUInt16();
        ColorPlaneCount = reader.ReadUInt16();
        BitsPerPixel = reader.ReadUInt16();
    }

    #endregion
}
