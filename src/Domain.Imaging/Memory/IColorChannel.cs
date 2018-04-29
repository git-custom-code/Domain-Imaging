namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for image color channel that allow acces to single <see cref="IColorChannelRow{T}"/>s.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    public interface IColorChannel<T> : IEnumerable<IColorChannelRow<T>>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets the <see cref="IColorChannelRow{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel row's index. </param>
        /// <returns> The <see cref="IColorChannelRow{T}"/> at the specified <paramref name="index"/>. </returns>
        IColorChannelRow<T> this[uint index] { get; }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        uint RowCount { get; }

        /// <summary>
        /// Convert the channel to a <see cref="Span{TType}"/>.
        /// </summary>
        /// <returns> A <see cref="Span{TType}"/>, i.e. a "managed pointer" to the channel. </returns>
        Span<TType> AsSpan<TType>() where TType : struct, IComparable, IConvertible, IFormattable;
    }
}