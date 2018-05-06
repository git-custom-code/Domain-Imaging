namespace CustomCode.Domain.Imaging.Memory
{
    using Data.Imaging.Memory;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Implementation for an image color channel row that allow acces to single color values.
    /// </summary>
    /// <typeparam name="T"> The color channel row's precision. </typeparam>
    public class ColorChannelRow<T> : IColorChannelRow<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ColorChannelRow{T}"/> type.
        /// </summary>
        /// <param name="channelIndex"> The index of the associated <see cref="IColorChannel{T}"/>. </param>
        /// <param name="rowIndex"> The row's index. </param>
        /// <param name="memory"> The associated memory that contains the image's pixel data. </param>
        public ColorChannelRow(byte channelIndex, uint rowIndex, IImageMemory memory)
        {
            Memory = memory;
            ChannelIndex = channelIndex;
            RowIndex = rowIndex;
            Count = Memory.SizePerAlignedRow - Memory.Stride;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the associated memory that contains the image's pixel data.
        /// </summary>
        protected IImageMemory Memory { get; }

        /// <summary>
        /// Gets the index of the associated <see cref="IColorChannel{T}"/>.
        /// </summary>
        protected byte ChannelIndex { get; }

        /// <summary>
        /// Gets the number of stored pixel values.
        /// </summary>
        public uint Count { get; protected set; }

        /// <summary>
        /// Gets the row's index.
        /// </summary>
        protected uint RowIndex { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Gets the color value at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color value's index. </param>
        /// <returns> The color value at the specified <paramref name="index"/>. </returns>
        object IColorChannelRow.this[uint index]
        {
            get { return this[index]; }
        }

        /// <summary>
        /// Gets the color value at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color value's index. </param>
        /// <returns> The color value at the specified <paramref name="index"/>. </returns>
        public virtual T this[uint index]
        {
            get
            {
                var start = (int)(ChannelIndex * Memory.SizePerChannel + RowIndex * Memory.SizePerAlignedRow);
                var length = (int)Memory.SizePerAlignedRow;
                var rowMemory = new Memory<byte>(Memory.AsArray(), start, length);
                var span = MemoryMarshal.Cast<byte, T>(rowMemory.Span);
                return span[(int)index];
            }
        }

        /// <summary>
        /// Convert the channel to a <see cref="Span{TType}"/>.
        /// </summary>
        /// <returns> A <see cref="Span{TType}"/>, i.e. a "managed pointer" to the channel. </returns>
        public Span<TType> AsSpan<TType>(bool ignoreStride = true)
            where TType : struct, IComparable, IConvertible, IFormattable
        {
            var start = (int)(ChannelIndex * Memory.SizePerChannel + RowIndex * Memory.SizePerAlignedRow);
            var length = (int)Memory.SizePerAlignedRow;
            if (ignoreStride == false)
            {
                length -= Memory.Stride;
            }
            var memory = new Memory<byte>(Memory.AsArray(), start, length);
            return MemoryMarshal.Cast<byte, TType>(memory.Span);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return new ColorChannelRowEnumerator<T>(ChannelIndex, RowIndex, Memory);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Creates a human readable string representation of this instance.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>
        public override string ToString()
        {
            return $"Row {RowIndex}: {Count} pixels";
        }

        #endregion
    }
}