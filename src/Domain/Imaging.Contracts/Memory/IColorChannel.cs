namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for image color channel that allow acces to single <see cref="IColorChannelRow"/>s.
    /// </summary>
    public interface IColorChannel : IEnumerable<IColorChannelRow>
    {
        /// <summary>
        /// Gets the <see cref="IColorChannelRow"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel row's index. </param>
        /// <returns> The <see cref="IColorChannelRow"/> at the specified <paramref name="index"/>. </returns>
        IColorChannelRow this[uint index] { get; }

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