namespace CustomCode.Data.Imaging;

using Memory;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Interface for an image file parser.
/// </summary>
public interface IImageFileParser
{
    /// <summary>
    /// Query if the parser can read image files with the specified <paramref name="fileExtension"/>.
    /// </summary>
    /// <param name="fileExtension"> The image's file extension. </param>
    /// <returns> True if the parser can read the specified <paramref name="fileExtension"/>, false otherwise. </returns>
    bool CanParse(string fileExtension);

    /// <summary>
    /// Parse an image file from a binary stream. 
    /// </summary>
    /// <param name="reader"> The reader to the binary image file stream. </param>
    /// <param name="alignment"> The desired memory alignment of the parsed <see cref="IImageMemory"/>. </param>
    /// <returns> The parsed <see cref="IImageMemory"/>. </returns>
    IImageMemory Parse(BinaryReader reader, MemoryAlignment alignment);

    /// <summary>
    /// Asynchronously parse an image file from a binary stream. 
    /// </summary>
    /// <param name="reader"> The reader to the binary image file stream. </param>
    /// <param name="alignment"> The desired memory alignment of the parsed <see cref="IImageMemory"/>. </param>
    /// <param name="token"> A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation. </param>
    /// <returns> An awaitable task with the parsed <see cref="IImageMemory"/>. </returns>
    Task<IImageMemory> ParseAsync(BinaryReader reader, MemoryAlignment alignment, CancellationToken? token = null);
}
