namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Data.Imaging.Memory;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ColorChannelBitCollection"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class ColorChannelBitCollectionTests : TestCase
    {
        [Fact(DisplayName = "Color channel collection Monochrome, 1bit")]
        public void CreateColorChannelCollectionMonochrome1Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .When(memory => new ColorChannelBitCollection(memory))
            .Then(channels => channels.Count.Should().Be(1));
        }
    }
}