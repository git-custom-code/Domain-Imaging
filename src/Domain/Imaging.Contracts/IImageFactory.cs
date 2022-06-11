namespace CustomCode.Domain.Imaging;

using Data.Imaging.Memory;

/// <summary>
/// Interface for a factory that can create <see cref="IImage"/> instances.
/// </summary>
public interface IImageFactory
{
    /// <summary>
    /// Create a new <see cref="IImage"/> instance from an already loaded <see cref="IImageMemory"/>.
    /// </summary>
    /// <param name="memory"> The image's already loaded pixel data. </param>
    /// <returns> The newly created image instance. </returns>
    IImage Create(IImageMemory memory);
}
