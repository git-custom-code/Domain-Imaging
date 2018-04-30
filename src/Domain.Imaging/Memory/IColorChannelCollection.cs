namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection that grants easy acces to the <see cref="IColorChannel{T}"/>s of an <see cref="IImageMemory"/>.
    /// </summary>
    /// <typeparam name="T"> The precision of a <see cref="IColorChannel{T}"/> entry. </typeparam>
    public interface IColorChannelCollection<T> : IEnumerable<IColorChannel<T>>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets the <see cref="IColorChannel{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel's index. </param>
        /// <returns> The <see cref="IColorChannel{T}"/> at the specified <paramref name="index"/>. </returns>
        IColorChannel<T> this[byte index] { get; }

        /// <summary>
        /// Gets the number of <see cref="IColorChannel{T}"/> within the collection.
        /// </summary>
        byte Count { get; }
    }
}