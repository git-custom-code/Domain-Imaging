namespace CustomCode.Data.Imaging.Bmp;

using System.Collections.Generic;

/// <summary>
/// Interface for a 24bit rgb color table that is stored in a bitmap file.
/// </summary>
public interface IColorTable : IList<(byte red, byte green, byte blue)>
{
    /// <summary>
    /// Query if the color table contains only the colors black and white.
    /// </summary>
    /// <returns> True if the color table contains only black and white, false otherwise. </returns>
    bool IsMonochrome();

    /// <summary>
    /// Query if the color table contains only gray scale colors.
    /// </summary>
    /// <returns> True if the color table contains only gray scale colors, false otherwise. </returns>
    bool IsGrayScale();
}
