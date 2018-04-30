namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A collection that grants easy acces to the <see cref="IColorChannel{T}"/>s of an <see cref="ImageMemory"/>.
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

        /// <summary>
        /// Gets the <see cref="IColorChannel{T}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel's index. </param>
        /// <returns> The <see cref="IColorChannel{T}"/> at the specified <paramref name="index"/>. </returns>
        public IColorChannel<T> this[byte index]
        {
            get
            {
                return Channels.Value[index];
            }
        }

        /// <summary>
        /// Gets the associated memory that contains the image's pixel data.
        /// </summary>
        protected IImageMemory Memory { get; }

        /// <summary>
        /// Gets the internal collection of <see cref="IColorChannel{T}"/>s.
        /// </summary>
        private Lazy<List<IColorChannel<T>>> Channels { get; }

        /// <summary>
        /// Gets the number of <see cref="IColorChannel{T}"/> within the collection.
        /// </summary>
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

            for (var i = 0ul; i < Memory.Size; i += Memory.SizePerChannel)
            {
                result.Add(new ColorChannel<T>(index, Memory));
                ++index;
            }

            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<IColorChannel<T>> GetEnumerator()
        {
            foreach (var channel in Channels.Value)
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