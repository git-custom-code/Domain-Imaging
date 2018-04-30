namespace CustomCode.Domain.Imaging.Memory
{
    using System.Collections.Generic;

    /// <summary>
    /// Specialized <see cref="ColorChannel{T}"/> for the <see cref="Bit"/> data type.
    /// </summary>
    public sealed class ColorChannelBit : ColorChannel<Bit>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ColorChannelBit"/> type.
        /// </summary>
        /// <param name="index"> The channel's index related to the associated <paramref name="buffer"/>. </param>
        /// <param name="buffer"> The associated memory buffer that contains the image's pixel data. </param>
        public ColorChannelBit(byte index, IImageMemoryBuffer buffer)
            : base(index, buffer)
        { }

        #endregion

        #region Logic

        /// <summary>
        /// Build the internal <see cref="IColorChannelRow{T}"/> collection.
        /// </summary>
        /// <returns> The internal <see cref="IColorChannelRow{T}"/> collection. </returns>
        protected override List<IColorChannelRow<Bit>> BuildRows()
        {
            var result = new List<IColorChannelRow<Bit>>();
            for (var i = 0u; i < RowCount; ++i)
            {
                result.Add(new ColorChannelBitRow(Index, i, Buffer));
            }
            return result;
        }

        #endregion
    }
}