namespace CustomCode.Data.Imaging.Bmp.Tests;

using Bmp;
using Memory;
using Memory.Bmp;
using System.IO;
using Test.BehaviorDrivenDevelopment;
using Xunit;

/// <summary>
/// Integration tests for the <see cref="BitmapParser"/> type.
/// </summary>
[IntegrationTest]
public class BitmapParserTests : TestCase
{
    [Fact(DisplayName = "Parse a monochrome bitmap with 1bit depth and black/white color palette")]
    public void ParseBitmapMonochromeWithBlackWhitePalette()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal1.bmp")))
        .When((parser, reader) => parser.Parse(reader, MemoryAlignment.None))
        .Then(memory =>
            {
                memory.Size.Should().Be(1024);
                memory.SizePerAlignedRow.Should().Be(16);
                memory.SizePerChannel.Should().Be(16 * 64);
                memory.SizePerPixel.Should().Be(127);
                memory.Stride.Should().Be(0);
            });
    }

    [Fact(DisplayName = "Parse a monochrome bitmap with 1bit depth and white/black color palette")]
    public void ParseBitmapMonochromeWithWhiteBlackColorPalette()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal1wb.bmp")))
        .When((parser, reader) => parser.Parse(reader, MemoryAlignment.None))
        .Then(memory =>
            {
                memory.Size.Should().Be(1024);
                memory.SizePerAlignedRow.Should().Be(16);
                memory.SizePerChannel.Should().Be(16 * 64);
                memory.SizePerPixel.Should().Be(127);
                memory.Stride.Should().Be(0);
            });
    }

    [Fact(DisplayName = "Parse a monochrome bitmap with 1bit depth and rgb color palette")]
    public void ParseBitmapMonochromeWithRgbColorPalette()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal1bg.bmp")))
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

    [Fact(DisplayName = "Parse a rgb bitmap with 4bit depth")]
    public void ParseBitmapRgb4()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal4.bmp")))
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

    [Fact(DisplayName = "Parse a gray scale bitmap with 4bit depth")]
    public void ParseBitmapGrayScale4()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal4gs.bmp")))
        .When((parser, reader) => parser.Parse(reader, MemoryAlignment.None))
        .Then(memory =>
        {
            memory.Size.Should().Be(127 * 64);
            memory.SizePerAlignedRow.Should().Be(127);
            memory.SizePerChannel.Should().Be(127 * 64);
            memory.SizePerPixel.Should().Be(1);
            memory.Stride.Should().Be(0);
        });
    }

    [Fact(DisplayName = "Parse a rgb bitmap with 4bit depth and runtime length encoding")]
    public void ParseBitmapRgb4WithRuntimeLengthEncoding()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal4rle.bmp")))
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

    [Fact(DisplayName = "Parse a rgb bitmap with 8bit depth")]
    public void ParseBitmapRgb8()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal8.bmp")))
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

    [Fact(DisplayName = "Parse a rgb bitmap with 8bit depth and every bmp field defaulted to ß")]
    public void ParseBitmapRgb8WithDefaultZero()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal8-0.bmp")))
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

    [Fact(DisplayName = "Parse a gray scale bitmap with 8bit depth")]
    public void ParseBitmapGrayScale8()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\pal8gs.bmp")))
        .When((parser, reader) => parser.Parse(reader, MemoryAlignment.None))
        .Then(memory =>
        {
            memory.Size.Should().Be(127 * 64);
            memory.SizePerAlignedRow.Should().Be(127);
            memory.SizePerChannel.Should().Be(127 * 64);
            memory.SizePerPixel.Should().Be(1);
            memory.Stride.Should().Be(0);
        });
    }

    [Fact(DisplayName = "Parse a rgb bitmap with 24bit depth")]
    [IntegrationTest]
    public void ParseBitmapRgb24()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
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

    [Fact(DisplayName = "Parse a rgb bitmap with 24bit depth and color palette")]
    public void ParseBitmapRgb24WithColorPalette()
    {
        Given(() => new BitmapParser(new MemoryParserFactory()))
        .Also(() => new BinaryReader(File.OpenRead(@".\Data\Valid\rgb24pal.bmp")))
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
