namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A collection that grants easy acces to the <see cref="ColorChannel{T}"/>s of an <see cref="ImageMemoryBuffer"/>.
    /// </summary>
    /// <typeparam name="T"> The precision of a <see cref="ColorChannel{T}"/> entry. </typeparam>
    public sealed class ColorChannelCollection<T> : IColorChannelCollection<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ColorChannelCollection{T}"/> type.
        /// </summary>
        /// <param name="buffer"> The image memory buffer that contains the color channel/pixe. data. </param>
        public ColorChannelCollection(IImageMemoryBuffer buffer)
        {
            byte index = 0;
            for (var i = 0ul; i < buffer.Count; i += buffer.SizePerChannel)
            {
                Channels.Add(new ColorChannel<T>(index, buffer));
                ++index;
            }
            Count = index;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the <see cref="ColorChannel{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel's index. </param>
        /// <returns> The <see cref="ColorChannel{T}"/> at the specified <paramref name="index"/>. </returns>
        public ColorChannel<T> this[byte index]
        {
            get
            {
                return Channels[(int)index];
            }
        }

        /// <summary>
        /// Gets the internal collection of <see cref="ColorChannel{T}"/>s.
        /// </summary>
        private List<ColorChannel<T>> Channels { get; } = new List<ColorChannel<T>>();

        /// <summary>
        /// Gets the number of <see cref="ColorChannel{T}"/> within the collection.
        /// </summary>
        public byte Count { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<ColorChannel<T>> GetEnumerator()
        {
            foreach (var channel in Channels)
            {
                yield return channel;
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
            return $"{Count} channels";
        }

        #endregion
    }
}