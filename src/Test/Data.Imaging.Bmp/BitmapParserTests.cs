namespace CustomCode.Data.Imaging.Tiff.Tests
{
    using Bmp;
    using Memory;
    using System.IO;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Integration tests for the <see cref="BitmapParser"/> type.
    /// </summary>
    [IntegrationTest]
    public class BitmapParserTests : TestCase
    {
        [Fact(DisplayName = "Parse a rgb bitmap with 24bit depth")]
        [IntegrationTest]
        public void ParseBitmapRgb24()
        {
            Given(() => new BitmapParser())
            .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\rgb24.bmp")))
            .When((parser, reader) => parser.Parse(reader, MemoryAlignment.None))
            .Then(memory =>
                {
                    memory.Size.Should().Be(127 * 64 * 3);
                    memory.SizePerAlignedRow.Should().Be(127);
                    memory.SizePerChannel.Should().Be(127 * 64);
                    memory.SizePerPixel.Should().Be(3);
                    memory.Stride.Should().Be(0);
                });
        }
    }
}