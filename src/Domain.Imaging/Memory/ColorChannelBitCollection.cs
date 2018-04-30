namespace CustomCode.Domain.Imaging.Memory
{
    using System.Collections.Generic;

    /// <summary>
    /// Specialized <see cref="ColorChannelCollection{T}"/> for the <see cref="Bit"/> data type.
    /// </summary>
    public sealed class ColorChannelBitCollection : ColorChannelCollection<Bit>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ColorChannelBitCollection"/> type.
        /// </summary>
        /// <param name="buffer"> The image memory buffer that contains the color channel/pixe. data. </param>
        public ColorChannelBitCollection(IImageMemoryBuffer buffer)
            : base(buffer)
        { }

        #endregion

        #region Logic

        /// <summary>
        /// Build the internal <see cref="IColorChannel{Bit}"/> collection.
        /// </summary>
        /// <returns> The internal <see cref="IColorChannel{Bit}"/> collection. </returns>
        protected override List<IColorChannel<Bit>> BuildChannels()
        {
            var result = new List<IColorChannel<Bit>>();
            byte index = 0;

            for (var i = 0ul; i < Buffer.Count; i += Buffer.SizePerChannel)
            {
                result.Add(new ColorChannelBit(index, Buffer));
                ++index;
            }

            return result;
        }

        #endregion
    }
}