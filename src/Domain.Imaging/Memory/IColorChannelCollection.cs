namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection that grants easy acces to the <see cref="ColorChannel{T}"/>s of an <see cref="ImageMemoryBuffer"/>.
    /// </summary>
    /// <typeparam name="T"> The precision of a <see cref="ColorChannel{T}"/> entry. </typeparam>
    public interface IColorChannelCollection<T> : IEnumerable<ColorChannel<T>>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets the <see cref="ColorChannel{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel's index. </param>
        /// <returns> The <see cref="ColorChannel{T}"/> at the specified <paramref name="index"/>. </returns>
        ColorChannel<T> this[byte index] { get; }

        /// <summary>
        /// Gets the number of <see cref="ColorChannel{T}"/> within the collection.
        /// </summary>
        byte Count { get; }
    }
}