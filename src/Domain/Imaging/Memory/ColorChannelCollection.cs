namespace CustomCode.Domain.Imaging.Memory;

using Data.Imaging.Memory;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A collection that grants easy access to the <see cref="IColorChannel{T}"/>s of an <see cref="IImageMemory"/>.
/// </summary>
/// <typeparam name="T"> The precision of a <see cref="IColorChannel{T}"/> entry. </typeparam>
public class ColorChannelCollection<T> : IColorChannelCollection<T>
    where T : struct, IComparable, IConvertible, IFormattable
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ColorChannelCollection{T}"/> type.
    /// </summary>
    /// <param name="memory"> The image memory that contains the color channel/pixel data. </param>
    public ColorChannelCollection(IImageMemory memory)
    {
        Memory = memory;
        Channels = new Lazy<List<IColorChannel<T>>>(BuildChannels, true);
        Count = (byte)(Memory.Size / Memory.SizePerChannel);
    }

    #endregion

    #region Data

    /// <inheritdoc cref="IColorChannelCollection" />
    IColorChannel IColorChannelCollection.this[byte index]
    {
        get { return this[index]; }
    }

    /// <inheritdoc cref="IColorChannelCollection{T}" />
    public IColorChannel<T> this[byte index]
    {
        get { return Channels.Value[index]; }
    }

    /// <summary>
    /// Gets the associated memory that contains the image's pixel data.
    /// </summary>
    protected IImageMemory Memory { get; }

    /// <summary>
    /// Gets the internal collection of <see cref="IColorChannel{T}"/>s.
    /// </summary>
    private Lazy<List<IColorChannel<T>>> Channels { get; }

    /// <inheritdoc cref="IColorChannelCollection" />
    public byte Count { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Build the internal <see cref="IColorChannel{T}"/> collection.
    /// </summary>
    /// <returns> The internal <see cref="IColorChannel{T}"/> collection. </returns>
    protected virtual List<IColorChannel<T>> BuildChannels()
    {
        var result = new List<IColorChannel<T>>();
        byte index = 0;

        for (var i = 0u; i < Memory.Size; i += Memory.SizePerChannel)
        {
            result.Add(new ColorChannel<T>(index, Memory));
            ++index;
        }

        return result;
    }

    /// <inheritdoc cref="IColorChannel{T}" />
    public IEnumerator<IColorChannel<T>> GetEnumerator()
    {
        foreach (var channel in Channels.Value)
        {
            yield return channel;
        }
    }

    /// <inheritdoc cref="IColorChannel" />
    IEnumerator<IColorChannel> IEnumerable<IColorChannel>.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="IEnumerable" />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        if (Count == 1)
        {
            return $"1 channel";
        }
        return $"{Count} channels";
    }

    #endregion
}
