namespace CustomCode.Domain.Imaging.Memory;

using Data.Imaging.Memory;
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
    /// <param name="memory"> The image memory that contains the color channel/pixe. data. </param>
    public ColorChannelBitCollection(IImageMemory memory)
        : base(memory)
    { }

    #endregion

    #region Logic

    /// <inheritdoc cref="ColorChannelCollection{T}"/>
    protected override List<IColorChannel<Bit>> BuildChannels()
    {
        var result = new List<IColorChannel<Bit>>();
        byte index = 0;

        for (var i = 0ul; i < Memory.Size; i += Memory.SizePerChannel)
        {
            result.Add(new ColorChannelBit(index, Memory));
            ++index;
        }

        return result;
    }

    #endregion
}