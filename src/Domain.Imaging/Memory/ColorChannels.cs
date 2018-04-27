namespace CustomCode.Domain.Imaging.Memory
{
    /// <summary>
    /// The number of used color channels per pixel of a <see cref="ImageMemoryBuffer"/>.
    /// </summary>
    public enum ColorChannels : byte
    {
        /// <summary> Single black and white channel buffer. </summary>
        Monochrome = 0,
        /// <summary> Single gray channel buffer. </summary>
        Gray = 1,
        /// <summary> Gray and alpha channel buffer. </summary>
        GrayAlpha = 2,
        /// <summary>  Red, green and blue channel buffer. </summary>
        Rgb = 3,
        /// <summary> Red, green, blue and alpha channel buffer. </summary>
        Rgba = 4
    }
}