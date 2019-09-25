namespace CustomCode.Domain.Imaging.Memory
{
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

        /// <inheritdoc />
        IColorChannel IColorChannelCollection.this[byte index]
        {
            get { return this[index]; }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerator<IColorChannel<T>> GetEnumerator()
        {
            foreach (var channel in Channels.Value)
            {
                yield return channel;
            }
        }

        /// <inheritdoc />
        IEnumerator<IColorChannel> IEnumerable<IColorChannel>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Count} channels";
        }

        #endregion
    }
}