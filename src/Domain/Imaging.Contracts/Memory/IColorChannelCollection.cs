namespace CustomCode.Domain.Imaging.Memory
{
    using Data.Imaging.Memory;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection that grants easy acces to the <see cref="IColorChannel"/>s of an <see cref="IImageMemory"/>.
    /// </summary>
    public interface IColorChannelCollection : IEnumerable<IColorChannel>
    {
        /// <summary>
        /// Gets the <see cref="IColorChannel"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The color channel's index. </param>
        /// <returns> The <see cref="IColorChannel"/> at the specified <paramref name="index"/>. </returns>
        IColorChannel this[byte index] { get; }

        /// <summary>
        /// Gets the number of <see cref="IColorChannel"/> within the collection.
        /// </summary>
        byte Count { get; }
    }
}