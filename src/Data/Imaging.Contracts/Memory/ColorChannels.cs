namespace CustomCode.Data.Imaging.Memory;

/// <summary>
/// The number of used color channels per pixel of a <see cref="IImageMemory"/>.
/// </summary>
public enum ColorChannels : byte
{
    /// <summary> Single black and white channel memory. </summary>
    Monochrome = 0,
    /// <summary> Single gray channel memory. </summary>
    Gray = 1,
    /// <summary> Gray and alpha channel memory. </summary>
    GrayAlpha = 2,
    /// <summary>  Red, green and blue channel memory. </summary>
    Rgb = 3,
    /// <summary> Red, green, blue and alpha channel memory. </summary>
    Rgba = 4
}
