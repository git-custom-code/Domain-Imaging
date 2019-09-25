namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Data.Imaging.Memory;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ColorChannelBit"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class ColorChannelBitTests : TestCase
    {
        [Fact(DisplayName = "Color channel Gray, 8bit")]
        public void CreateColorChannelGray8Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .When(memory => new ColorChannelBit(0, memory))
            .Then(channel => channel.RowCount.Should().Be(20));
        }
    }
}