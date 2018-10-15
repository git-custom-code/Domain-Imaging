namespace CustomCode.Data.Imaging.Memory.Bmp
{
    using System.IO;

    /// <summary>
    /// Interface for a parser that can read raw bitmap pixel data and return an <see cref="IImageMemory"/>
    /// representation.
    /// </summary>
    public interface IMemoryParser
    {
        /// <summary>
        /// Parse raw bitmap pixel data as <see cref="IImageMemory"/>.
        /// </summary>
        /// <param name="reader"> The binary reader to the raw bitmap pixel data. </param>
        /// <returns> The parsed pixel data as <see cref="IImageMemory"/>. </returns>
        IImageMemory Parse(BinaryReader reader);
    }
}