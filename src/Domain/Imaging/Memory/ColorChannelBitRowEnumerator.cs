namespace CustomCode.Domain.Imaging.Memory;

using Data.Imaging.Memory;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An <see cref="IEnumerator{T}"/> implementation over the color values of an <see cref="ColorChannelBitRow"/>
/// </summary>
public class ColorChannelBitRowEnumerator : IEnumerator<Bit>
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ColorChannelRowEnumerator{T}"/> type.
    /// </summary>
    /// <param name="channelIndex"> The index of the associated <see cref="IColorChannel{T}"/>. </param>
    /// <param name="rowIndex"> The index of the associated <see cref="IColorChannelRow{T}"/>. </param>
    /// <param name="memory"> The associated memory that contains the image's pixel data. </param>
    public ColorChannelBitRowEnumerator(byte channelIndex, uint rowIndex, IImageMemory memory)
    {
        var start = (int)(channelIndex * memory.SizePerChannel + rowIndex * memory.SizePerAlignedRow);
        var length = (int)memory.SizePerAlignedRow;
        Memory = new ReadOnlyMemory<byte>(memory.AsArray(), start, length);
        RowLength = memory.SizePerPixel;
    }

    #endregion

    #region Data

    /// <inheritdoc cref="IEnumerator{T}"/>
    public Bit Current { get; private set; }

    /// <inheritdoc cref="IEnumerator"/>
    object IEnumerator.Current { get { return Current; } }

    /// <summary>
    /// Gets the current (color) index within the associated <see cref="IColorChannelRow{T}"/>.
    /// </summary>
    private uint Index { get; set; }

    /// <summary>
    /// Gets the associated <see cref="ColorChannelBitRow"/> data.
    /// </summary>
    private ReadOnlyMemory<byte> Memory { get; }

    /// <summary>
    /// Gets the number of color values inside the associated <see cref="Memory"/>.
    /// </summary>
    private uint RowLength { get; set; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IDisposable"/>
    public void Dispose()
    { }

    /// <inheritdoc cref="IEnumerator{T}"/>
    public bool MoveNext()
    {
        if (Index < RowLength)
        {
            var byteIndex = (int)(Index / 8);
            var bitIndex = 7 - (int)(Index - 8 * byteIndex);
            var currentByte = Memory.Span[byteIndex];
            var bitValue = (currentByte & (1 << bitIndex)) != 0;
            Current = new Bit(bitValue);
            ++Index;
            return true;
        }

        return false;
    }

    /// <inheritdoc cref="IEnumerator{T}"/>
    public void Reset()
    {
        Index = 0;
        Current = default(Bit);
    }

    #endregion
}
