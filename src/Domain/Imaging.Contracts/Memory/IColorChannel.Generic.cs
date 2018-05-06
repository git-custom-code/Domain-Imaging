namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for image color channel that allow acces to single <see cref="IColorChannelRow{T}"/>s.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    public interface IColorChannel<T> : IColorChannel, IEnumerable<IColorChannelRow<T>>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets the <see cref="IColorChannelRow{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel row's index. </param>
        /// <returns> The <see cref="IColorChannelRow{T}"/> at the specified <paramref name="index"/>. </returns>
        new IColorChannelRow<T> this[uint index] { get; }
    }
}