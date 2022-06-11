namespace CustomCode.Data.Imaging.Memory.Tests;

using Xunit;

public sealed class ImageMemoryTests
{
    #region Monochrome, 1Bit

    [Fact(DisplayName = "Image memory: Monochrome, 1Bit, Unaligned")]
    public void MonochromeOneBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Monochrome, MemoryPrecision.OneBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Monochrome, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.OneBit, memory.Precision);
        Assert.Equal(40u, memory.Size);
        Assert.Equal(2u, memory.SizePerAlignedRow);
        Assert.Equal(40u, memory.SizePerChannel);
        Assert.Equal(10u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Monochrome, 1Bit, Aligned at 32Bit")]
    public void MonochromeOneBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Monochrome, MemoryPrecision.OneBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Monochrome, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.OneBit, memory.Precision);
        Assert.Equal(80u, memory.Size);
        Assert.Equal(4u, memory.SizePerAlignedRow);
        Assert.Equal(80u, memory.SizePerChannel);
        Assert.Equal(10u, memory.SizePerPixel);
        Assert.Equal(2u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Monochrome, 1Bit, Aligned at 64Bit")]
    public void MonochromeOneBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Monochrome, MemoryPrecision.OneBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Monochrome, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.OneBit, memory.Precision);
        Assert.Equal(160u, memory.Size);
        Assert.Equal(8u, memory.SizePerAlignedRow);
        Assert.Equal(160u, memory.SizePerChannel);
        Assert.Equal(10u, memory.SizePerPixel);
        Assert.Equal(6u, memory.Stride);
    }

    #endregion

    #region Gray, 8Bit

    [Fact(DisplayName = "Image memory: Gray, 8Bit, Unaligned")]
    public void GrayEightBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Gray, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(200u, memory.Size);
        Assert.Equal(10u, memory.SizePerAlignedRow);
        Assert.Equal(200u, memory.SizePerChannel);
        Assert.Equal(1u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Gray, 8Bit, Aligned at 32Bit")]
    public void GrayEightBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Gray, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Gray, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(240u, memory.Size);
        Assert.Equal(12u, memory.SizePerAlignedRow);
        Assert.Equal(240u, memory.SizePerChannel);
        Assert.Equal(1u, memory.SizePerPixel);
        Assert.Equal(2u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Gray, 8Bit, Aligned at 64Bit")]
    public void GrayEightBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Gray, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Gray, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(320u, memory.Size);
        Assert.Equal(16u, memory.SizePerAlignedRow);
        Assert.Equal(320u, memory.SizePerChannel);
        Assert.Equal(1u, memory.SizePerPixel);
        Assert.Equal(6u, memory.Stride);
    }

    #endregion

    #region Gray, 16Bit

    [Fact(DisplayName = "Image memory: Gray, 16Bit, Unaligned")]
    public void GraySixteenBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Gray, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(400u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(2u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Gray, 16Bit, Aligned at 32Bit")]
    public void GraySixteenBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Gray, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Gray, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(400u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(2u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Gray, 16Bit, Aligned at 64Bit")]
    public void GraySixteenBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Gray, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Gray, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(480u, memory.Size);
        Assert.Equal(24u, memory.SizePerAlignedRow);
        Assert.Equal(480u, memory.SizePerChannel);
        Assert.Equal(2u, memory.SizePerPixel);
        Assert.Equal(4u, memory.Stride);
    }

    #endregion

    #region GrayAlpha, 8Bit

    [Fact(DisplayName = "Image memory: GrayAlpha, 8Bit, Unaligned")]
    public void GrayAlphaEightBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.GrayAlpha, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(400u, memory.Size);
        Assert.Equal(10u, memory.SizePerAlignedRow);
        Assert.Equal(200u, memory.SizePerChannel);
        Assert.Equal(2u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: GrayAlpha, 8Bit, Aligned at 32Bit")]
    public void GrayAlphaEightBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.GrayAlpha, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.GrayAlpha, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(480u, memory.Size);
        Assert.Equal(12u, memory.SizePerAlignedRow);
        Assert.Equal(240u, memory.SizePerChannel);
        Assert.Equal(2u, memory.SizePerPixel);
        Assert.Equal(2u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: GrayAlpha, 8Bit, Aligned at 64Bit")]
    public void GrayAlphaEightBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.GrayAlpha, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.GrayAlpha, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(640u, memory.Size);
        Assert.Equal(16u, memory.SizePerAlignedRow);
        Assert.Equal(320u, memory.SizePerChannel);
        Assert.Equal(2u, memory.SizePerPixel);
        Assert.Equal(6u, memory.Stride);
    }

    #endregion

    #region GrayAlpha, 16Bit

    [Fact(DisplayName = "Image memory: GrayAlpha, 16Bit, Unaligned")]
    public void GrayAlphaSixteenBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.GrayAlpha, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(800u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(4u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: GrayAlpha, 16Bit, Aligned at 32Bit")]
    public void GrayAlphaSixteenBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.GrayAlpha, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(800u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(4u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: GrayAlpha, 16Bit, Aligned at 64Bit")]
    public void GrayAlphaSixteenBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.GrayAlpha, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(960u, memory.Size);
        Assert.Equal(24u, memory.SizePerAlignedRow);
        Assert.Equal(480u, memory.SizePerChannel);
        Assert.Equal(4u, memory.SizePerPixel);
        Assert.Equal(4u, memory.Stride);
    }

    #endregion

    #region Rgb, 8Bit

    [Fact(DisplayName = "Image memory: Rgb, 8Bit, Unaligned")]
    public void RgbEightBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Rgb, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(600u, memory.Size);
        Assert.Equal(10u, memory.SizePerAlignedRow);
        Assert.Equal(200u, memory.SizePerChannel);
        Assert.Equal(3u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgb, 8Bit, Aligned at 32Bit")]
    public void RgbEightBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Rgb, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgb, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(720u, memory.Size);
        Assert.Equal(12u, memory.SizePerAlignedRow);
        Assert.Equal(240u, memory.SizePerChannel);
        Assert.Equal(3u, memory.SizePerPixel);
        Assert.Equal(2u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgb, 8Bit, Aligned at 64Bit")]
    public void RgbEightBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Rgb, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgb, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(960u, memory.Size);
        Assert.Equal(16u, memory.SizePerAlignedRow);
        Assert.Equal(320u, memory.SizePerChannel);
        Assert.Equal(3u, memory.SizePerPixel);
        Assert.Equal(6u, memory.Stride);
    }

    #endregion

    #region Rgb, 16Bit

    [Fact(DisplayName = "Image memory: Rgb, 16Bit, Unaligned")]
    public void RgbSixteenBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Rgb, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(1200u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(6u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgb, 16Bit, Aligned at 32Bit")]
    public void RgbSixteenBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Rgb, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgb, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(1200u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(6u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgb, 16Bit, Aligned at 64Bit")]
    public void RgbSixteenBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Rgb, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgb, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(1440u, memory.Size);
        Assert.Equal(24u, memory.SizePerAlignedRow);
        Assert.Equal(480u, memory.SizePerChannel);
        Assert.Equal(6u, memory.SizePerPixel);
        Assert.Equal(4u, memory.Stride);
    }

    #endregion

    #region Rgba, 8Bit

    [Fact(DisplayName = "Image memory: Rgba, 8Bit, Unaligned")]
    public void RgbaEightBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Rgba, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(800u, memory.Size);
        Assert.Equal(10u, memory.SizePerAlignedRow);
        Assert.Equal(200u, memory.SizePerChannel);
        Assert.Equal(4u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgba, 8Bit, Aligned at 32Bit")]
    public void RgbaEightBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Rgba, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgba, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(960u, memory.Size);
        Assert.Equal(12u, memory.SizePerAlignedRow);
        Assert.Equal(240u, memory.SizePerChannel);
        Assert.Equal(4u, memory.SizePerPixel);
        Assert.Equal(2u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgba, 8Bit, Aligned at 64Bit")]
    public void RgbaEightBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Rgba, MemoryPrecision.EightBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgba, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.EightBit, memory.Precision);
        Assert.Equal(1280u, memory.Size);
        Assert.Equal(16u, memory.SizePerAlignedRow);
        Assert.Equal(320u, memory.SizePerChannel);
        Assert.Equal(4u, memory.SizePerPixel);
        Assert.Equal(6u, memory.Stride);
    }

    #endregion

    #region Rgba, 16Bit

    [Fact(DisplayName = "Image memory: Rgba, 16Bit, Unaligned")]
    public void RgbaSixteenBitUnaligned()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.None, memory.Alignment);
        Assert.Equal(ColorChannels.Rgba, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(1600u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(8u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgba, 16Bit, Aligned at 32Bit")]
    public void RgbaSixteenBitAlignedAt32Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At32Bit, ColorChannels.Rgba, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At32Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgba, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(1600u, memory.Size);
        Assert.Equal(20u, memory.SizePerAlignedRow);
        Assert.Equal(400u, memory.SizePerChannel);
        Assert.Equal(8u, memory.SizePerPixel);
        Assert.Equal(0u, memory.Stride);
    }

    [Fact(DisplayName = "Image memory: Rgba, 16Bit, Aligned at 64Bit")]
    public void RgbaSixteenBitAlignedAt64Bit()
    {
        // Given

        // When
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.At64Bit, ColorChannels.Rgba, MemoryPrecision.SixteenBit);

        // Then
        Assert.Equal(MemoryAlignment.At64Bit, memory.Alignment);
        Assert.Equal(ColorChannels.Rgba, memory.ColorChannels);
        Assert.Equal(MemoryPrecision.SixteenBit, memory.Precision);
        Assert.Equal(1920u, memory.Size);
        Assert.Equal(24u, memory.SizePerAlignedRow);
        Assert.Equal(480u, memory.SizePerChannel);
        Assert.Equal(8u, memory.SizePerPixel);
        Assert.Equal(4u, memory.Stride);
    }

    #endregion

    #region Conversions

    [Fact(DisplayName = "Image memory as array")]
    public void AsArray()
    {
        // Given
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit);

        // When
        var array = memory.AsArray();

        // Then
        Assert.NotNull(array);
        Assert.Equal(200, array.Length);
    }

    [Fact(DisplayName = "Image memory as span")]
    public void AsSpan()
    {
        // Given
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit);

        // When
        var span = memory.AsSpan();

        // Then
        Assert.Equal(200, span.Length);
    }

    [Fact(DisplayName = "Image memory as readonly span")]
    public void AsReadOnlySpan()
    {
        // Given
        var memory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit);

        // When
        var span = memory.AsReadOnlySpan();

        // Then
        Assert.Equal(200, span.Length);
    }

    [Fact(DisplayName = "Image memory as memory")]
    public void AsMemory()
    {
        // Given
        var imageMemory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit);

        // When
        var memory = imageMemory.AsMemory();

        // Then
        Assert.Equal(200, memory.Length);
    }

    [Fact(DisplayName = "Image memory as readonly memory")]
    public void AsReadOnlyMemory()
    {
        // Given
        var imageMemory = new ImageMemory((10u, 20u), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit);

        // When
        var memory = imageMemory.AsReadOnlyMemory();

        // Then
        Assert.Equal(200, memory.Length);
    }

    #endregion
}