namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implementation for an image color channel row that allow acces to single <see cref="Bit"/> color values.
    /// </summary>
    public sealed class ColorChannelBitRow : ColorChannelRow<Bit>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ColorChannelBitRow"/> type.
        /// </summary>
        /// <param name="channelIndex"> The index of the associated <see cref="IColorChannel{T}"/>. </param>
        /// <param name="rowIndex"> The row's index. </param>
        /// <param name="buffer"> The associated memory buffer that contains the image's pixel data. </param>
        public ColorChannelBitRow(byte channelIndex, uint rowIndex, IImageMemoryBuffer buffer)
            : base(channelIndex, rowIndex, buffer)
        {
            Count = buffer.SizePerPixel;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Gets the color value at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color value's index. </param>
        /// <returns> The color value at the specified <paramref name="index"/>. </returns>
        public override Bit this[uint index]
        {
            get
            {
                var start = (int)(ChannelIndex * Buffer.SizePerChannel + RowIndex * Buffer.SizePerAlignedRow);
                var length = (int)Buffer.SizePerAlignedRow;
                var rowMemory = new ReadOnlyMemory<byte>(Buffer.AsArray(), start, length);
                var byteIndex = (int)(index / 8);
                var bitIndex = (int)(index - 8 * byteIndex);
                var currentByte = rowMemory.Span[byteIndex];
                var bitValue = (currentByte & (1 << bitIndex)) != 0;
                return new Bit(bitValue);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public override IEnumerator<Bit> GetEnumerator()
        {
            return new ColorChannelBitRowEnumerator(ChannelIndex, RowIndex, Buffer);
        }

        #endregion
    }
}