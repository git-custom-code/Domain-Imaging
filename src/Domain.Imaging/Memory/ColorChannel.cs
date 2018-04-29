namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Implementation for image color channel that allow acces to single <see cref="IColorChannelRow{T}"/>s.
    /// </summary>
    /// <typeparam name="T"> The color channel's precision. </typeparam>
    public sealed class ColorChannel<T> : IColorChannel<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ColorChannel{T}"/> type.
        /// </summary>
        /// <param name="index"> The channel's index related to the associated <paramref name="buffer"/>. </param>
        /// <param name="buffer"> The associated memory buffer that contains the image's pixel data. </param>
        public ColorChannel(byte index, IImageMemoryBuffer buffer)
        {
            Index = index;
            Buffer = buffer;
            RowCount = (uint)(buffer.SizePerChannel / buffer.SizePerAlignedRow);
            Rows = new Lazy<List<IColorChannelRow<T>>>(BuildRows, true);
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the <see cref="IColorChannelRow{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel row's index. </param>
        /// <returns> The <see cref="IColorChannelRow{T}"/> at the specified <paramref name="index"/>. </returns>
        public IColorChannelRow<T> this[uint index]
        {
            get { return Rows.Value[(int)index]; }
        }

        /// <summary>
        /// Gets the associated memory buffer that contains the image's pixel data.
        /// </summary>
        private IImageMemoryBuffer Buffer { get; }

        /// <summary>
        /// Gets the channel's index related to the associated <paramref name="buffer"/>.
        /// </summary>
        private byte Index { get; }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public uint RowCount { get; }

        /// <summary>
        /// Gets a collection that contains the color channel's rows.
        /// </summary>
        private Lazy<List<IColorChannelRow<T>>> Rows { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert the channel to a <see cref="Span{TType}"/>.
        /// </summary>
        /// <returns> A <see cref="Span{TType}"/>, i.e. a "managed pointer" to the channel. </returns>
        public Span<TType> AsSpan<TType>()
            where TType : struct, IComparable, IConvertible, IFormattable
        {
            var start = (int)(Index * Buffer.SizePerChannel);
            var length = (int)Buffer.SizePerChannel;
            var memory = new Memory<byte>(Buffer.AsArray(), start, length);
            return MemoryMarshal.Cast<byte, TType>(memory.Span);
        }

        /// <summary>
        /// Build the internal <see cref="IColorChannelRow{T}"/> collection.
        /// </summary>
        /// <returns> The internal <see cref="IColorChannelRow{T}"/> collection. </returns>
        private List<IColorChannelRow<T>> BuildRows()
        {
            var result = new List<IColorChannelRow<T>>();
            for (var i = 0u; i < RowCount; ++i)
            {
                result.Add(new ColorChannelRow<T>(Index, i, Buffer));
            }
            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<IColorChannelRow<T>> GetEnumerator()
        {
            foreach (var row in Rows.Value)
            {
                yield return row;
            }
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
            if (Buffer.ColorChannels == ColorChannels.Monochrome)
            {
                return $"Monochrome ({RowCount} rows)";
            }
            else if (Buffer.ColorChannels == ColorChannels.Gray)
            {
                return $"Gray ({RowCount} rows)";
            }
            else if (Buffer.ColorChannels == ColorChannels.GrayAlpha)
            {
                if (Index == 0)
                {
                    return $"Gray ({RowCount} rows)";
                }
                return $"Alpha ({RowCount} rows)";
            }
            else if (Buffer.ColorChannels == ColorChannels.Rgb)
            {
                if (Index == 0)
                {
                    return $"Red ({RowCount} rows)";
                }
                else if (Index == 1)
                {
                    return $"Green ({RowCount} rows)";
                }
                return $"Blue ({RowCount} rows)";
            }
            else
            {
                if (Index == 0)
                {
                    return $"Red ({RowCount} rows)";
                }
                else if (Index == 1)
                {
                    return $"Green ({RowCount} rows)";
                }
                else if (Index == 2)
                {
                    return $"Blue ({RowCount} rows)";
                }
                return $"Alpha ({RowCount} rows)";
            }
        }

        #endregion
    }
}