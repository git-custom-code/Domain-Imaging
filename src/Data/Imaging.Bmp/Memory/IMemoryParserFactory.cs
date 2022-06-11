namespace CustomCode.Data.Imaging.Memory.Bmp;

using Imaging.Bmp;

/// <summary>
/// A factory that can be used to create new <see cref="IMemoryParser"/> instances.
/// </summary>
public interface IMemoryParserFactory
{
    /// <summary>
    /// Creates an appropriate <see cref="IMemoryParser"/> based on the information
    /// inside of the bitmap's <see cref="InfoHeader"/>.
    /// </summary>
    /// <param name="alignment"> The memory alignment of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="colorTable"> The bitmap's color table or null. </param>
    /// <param name="header"> The bitmap's <see cref="InfoHeader"/>. </param>
    /// <returns> An <see cref="IMemoryParser"/> that can be used to parse the bitmap's raw pixel data. </returns>
    IMemoryParser Create(MemoryAlignment alignment, IColorTable colorTable, InfoHeader header);
}
