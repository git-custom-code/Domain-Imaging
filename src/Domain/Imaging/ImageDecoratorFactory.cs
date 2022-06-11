namespace CustomCode.Domain.Imaging;

using Core.Composition;
using Data.Imaging.Memory;
using LightInject;

/// <summary>
/// Implementation for a factory that can create <see cref="IImageDecorator"/> instances.
/// </summary>
[Export(typeof(IImageDecoratorFactory))]
public sealed class ImageDecoratorFactory : IImageDecoratorFactory
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ImageDecoratorFactory"/> type.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator that can be used to create <see cref="IImageDecoratorFactory{T}"/> instances.
    /// </param>
    public ImageDecoratorFactory(IServiceFactory serviceLocator)
    {
        ServiceLocator = serviceLocator;
    }

    /// <summary>
    /// Gets the service locator that can be used to create <see cref="IImageDecoratorFactory{T}"/> instances.
    /// </summary>
    private IServiceFactory ServiceLocator { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IImageDecoratorFactory"/>
    public T Create<T>(IImage image, IImageMemory memory)
        where T : IImageDecorator
    {
        var factory = ServiceLocator.GetInstance<IImageDecoratorFactory<T>>();
        var decorator = factory.Create(image, memory);
        return decorator;
    }

    #endregion
}
