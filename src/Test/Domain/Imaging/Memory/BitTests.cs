namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="Bit"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class BitTests : TestCase
    {
        [Fact(DisplayName = "Convert zero bit to boolean")]
        public void ConvertZeroBitToBoolean()
        {
            Given(() => new Bit(false))
            .When(bit => bit.ToBoolean(null))
            .Then(@bool => @bool.Should().BeFalse());
        }

        [Fact(DisplayName = "Convert one bit to boolean")]
        public void ConvertOneBitToBoolean()
        {
            Given(() => new Bit(true))
            .When(bit => bit.ToBoolean(null))
            .Then(@bool => @bool.Should().BeTrue());
        }
    }
}