namespace CustomCode.Domain.Imaging;

/// <summary>
/// Interface for a image that contains pixel data.
/// </summary>
public interface IImage
{
    /// <summary>
    /// Gets the image's dimension (width and height in pixel).
    /// </summary>
    Dimension Dimension { get; }

    /// <summary>
    /// Cast the image to a specific <see cref="IImageDecorator"/> in order to use the decorated image's functionality.
    /// </summary>
    /// <typeparam name="T"> The type of the desired decorator. </typeparam>
    /// <returns> The decorated image. </returns>
    T As<T>() where T : IImageDecorator;
}
