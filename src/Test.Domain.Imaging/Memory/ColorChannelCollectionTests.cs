namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ColorChannelCollection{T}"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class ColorChannelCollectionTests : TestCase
    {
        [Fact(DisplayName = "Color channel collection Gray, 8bit")]
        public void CreateColorChannelCollectionGray8Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit))
            .When(memory => new ColorChannelCollection<byte>(memory))
            .Then(collection => collection.Count.Should().Be(1));
        }

        [Fact(DisplayName = "Color channel collection GrayAlpha, 8bit")]
        public void CreateColorChannelCollectionGrayAlpha8Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.EightBit))
            .When(memory => new ColorChannelCollection<byte>(memory))
            .Then(collection => collection.Count.Should().Be(2));
        }

        [Fact(DisplayName = "Color channelcollection  Rgb, 8bit")]
        public void CreateColorChannelCollectionRgb8Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.EightBit))
            .When(memory => new ColorChannelCollection<byte>(memory))
            .Then(collection => collection.Count.Should().Be(3));
        }

        [Fact(DisplayName = "Color channel collection Rgba, 8bit")]
        public void CreateColorChannelCollectionRgba8Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.EightBit))
            .When(memory => new ColorChannelCollection<byte>(memory))
            .Then(collection => collection.Count.Should().Be(4));
        }

        [Fact(DisplayName = "Color channel collection Gray, 16bit")]
        public void CreateColorChannelCollectionGray16Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.SixteenBit))
            .When(memory => new ColorChannelCollection<ushort>(memory))
            .Then(collection => collection.Count.Should().Be(1));
        }

        [Fact(DisplayName = "Color channel collection GrayAlpha, 16bit")]
        public void CreateColorChannelCollectionGrayAlpha16Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit))
            .When(memory => new ColorChannelCollection<ushort>(memory))
            .Then(collection => collection.Count.Should().Be(2));
        }

        [Fact(DisplayName = "Color channelcollection  Rgb, 16bit")]
        public void CreateColorChannelCollectionRgb16Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.SixteenBit))
            .When(memory => new ColorChannelCollection<ushort>(memory))
            .Then(collection => collection.Count.Should().Be(3));
        }

        [Fact(DisplayName = "Color channel collection Rgba, 16bit")]
        public void CreateColorChannelCollectionRgba16Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.SixteenBit))
            .When(memory => new ColorChannelCollection<ushort>(memory))
            .Then(collection => collection.Count.Should().Be(4));
        }
    }
}