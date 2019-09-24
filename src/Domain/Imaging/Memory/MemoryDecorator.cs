namespace CustomCode.Domain.Imaging.Memory
{
    using Data.Imaging.Memory;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Implementation for an <see cref="IImageDecorator"/> that allows direct access to an image's raw memory.
    /// </summary>
    public sealed class MemoryDecorator<T> : IMemory<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="MemoryDecorator{T}"/> type.
        /// </summary>
        /// <param name="memory"> The image's <see cref="IImageMemory"/> that contains the pixel data. </param>
        public MemoryDecorator(IImageMemory memory)
        {
            if (memory.Precision == MemoryPrecision.OneBit)
            {
                Channels = new ColorChannelBitCollection(memory) as IColorChannelCollection<T>;
            }
            else
            {
                Channels = new ColorChannelCollection<T>(memory);
            }
            Memory = memory;
        }

        /// <summary>
        /// Gets the image's <see cref="IImageMemory"/> that contains the pixel data.
        /// </summary>
        private IImageMemory Memory { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets the image's color channels.
        /// </summary>
        IColorChannelCollection IMemory.Channels
        {
            get { return Channels; }
        }

        /// <summary>
        /// Gets the image's color channels.
        /// </summary>
        public IColorChannelCollection<T> Channels { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Gets the image's raw memory as byte array.
        /// </summary>
        /// <returns> The image's raw memory as byte array. </returns>
        byte[] IMemory.AsArray()
        {
            return Memory.AsArray();
        }

        /// <summary>
        /// Gets the image's raw memory as array.
        /// </summary>
        /// <returns> The image's raw memory as array. </returns>
        public T[] AsArray()
        {
            var span = AsSpan();
            return span.ToArray();
        }

        /// <summary>
        /// Gets the image's raw memory as <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns> An <see cref="IEnumerable{T}"/> over the image's raw memory. </returns>
        IEnumerable<byte> IMemory.AsEnumerable()
        {
            return Memory.AsArray();
        }

        /// <summary>
        /// Gets the image's raw memory as <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns> An <see cref="IEnumerable{T}"/> over the image's raw memory. </returns>
        public IEnumerable<T> AsEnumerable()
        {
            var span = AsSpan();
            return span.ToArray();
        }

        /// <summary>
        /// Gets the image's raw memory as <see cref="System.Memory{T}"/>.
        /// </summary>
        /// <returns> The image's raw memory as <see cref="System.Memory{T}"/>. </returns>
        public Memory<T> AsMemory()
        {
            var array = AsArray();
            return new Memory<T>(array);
        }

        /// <summary>
        /// Gets the image's raw memory as <see cref="System.Span{T}"/>.
        /// </summary>
        /// <returns> The image's raw memory as <see cref="System.Span{T}"/>. </returns>
        public Span<T> AsSpan()
        {
            var memory = new Memory<byte>(Memory.AsArray());
            var span = MemoryMarshal.Cast<byte, T>(memory.Span);
            return span;
        }

        #endregion
    }
}