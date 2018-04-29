namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ColorChannel{T}"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class ColorChannelTests : TestCase
    {
        [Fact(DisplayName = "Color channel Monochrome, 1bit")]
        public void CreateColorChannelMonochrome1Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .When(buffer => new ColorChannel<byte>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel Gray, 8bit")]
        public void CreateColorChannelGray8Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit))
            .When(buffer => new ColorChannel<byte>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel GrayAlpha, 8bit")]
        public void CreateColorChannelGrayAlpha8Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.EightBit))
            .When(buffer => new ColorChannel<byte>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel Rgb, 8bit")]
        public void CreateColorChannelRgb8Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.EightBit))
            .When(buffer => new ColorChannel<byte>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel Rgba, 8bit")]
        public void CreateColorChannelRgba8Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.EightBit))
            .When(buffer => new ColorChannel<byte>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel Gray, 16bit")]
        public void CreateColorChannelGray16Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.SixteenBit))
            .When(buffer => new ColorChannel<ushort>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel GrayAlpha, 16bit")]
        public void CreateColorChannelGrayAlpha16Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit))
            .When(buffer => new ColorChannel<ushort>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel Rgb, 16bit")]
        public void CreateColorChannelRgb16Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.SixteenBit))
            .When(buffer => new ColorChannel<ushort>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }

        [Fact(DisplayName = "Color channel Rgba, 16bit")]
        public void CreateColorChannelRgba16Bit()
        {
            Given(() => new ImageMemoryBuffer((10, 20), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.SixteenBit))
            .When(buffer => new ColorChannel<ushort>(0, buffer))
            .Then(channel => channel.RowCount.Should().Be(20));
        }
    }
}