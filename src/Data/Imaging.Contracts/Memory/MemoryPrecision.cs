namespace CustomCode.Data.Imaging.Memory
{
    /// <summary>
    /// The precision of a single <see cref="IImageMemory"/> entry in bit.
    /// </summary>
    public enum MemoryPrecision : byte
    {
        /// <summary> One bit per color channel per pixel. </summary>
        OneBit = 0,
        /// <summary> Eight bit per color channel per pixel. </summary>
        EightBit = 1,
        /// <summary> Sixteen bit per color channel per pixel. </summary>
        SixteenBit = 2
    }
}