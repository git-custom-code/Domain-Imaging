namespace CustomCode.Data.Imaging.Memory;

/// <summary>
/// The pixel row alignment (at 0, 4 or 8 byte boundaries) of a <see cref="IImageMemory"/>.
/// </summary>
public enum MemoryAlignment : byte
{
    /// <summary> Don't align memory data rows. </summary>
    None = 1,
    /// <summary> Align a memory data row at 4 byte boundaries. </summary>
    At32Bit = 4,
    /// <summary> Align a memory data row at 8 byte boundaries. </summary>
    At64Bit = 8
}
