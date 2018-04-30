namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ColorChannelRowEnumerator{T}"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class ColorChannelRowEnumeratorTests : TestCase
    {
        [Fact(DisplayName = "Color channel collection Monochrome, 1bit")]
        public void CreateColorChannelCollectionMonochrome1Bit()
        {
            Given(() => new ImageMemory((10, 20), MemoryAlignment.None, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .When(memory => new ColorChannelCollection<byte>(memory))
            .Then(collection => collection.Count.Should().Be(1));
        }
    }
}