namespace CustomCode.Domain.Imaging.Memory.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ImageMemory"/> type.
    /// </summary>
    [UnitTest]
    [Category("Memory")]
    public sealed class ImageMemoryTests : TestCase
    {
        #region 1bit

        [Fact(DisplayName = "Monochrome, 1bit, None")]
        public void CreateMonochromeOneBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(33);
                });
        }

        [Fact(DisplayName = "Monochrome, 1bit, 32bit")]
        public void CreateMonochromeOneBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(44);
                });
        }

        [Fact(DisplayName = "Monochrome, 1bit, 64bit")]
        public void CreateMonochromeOneBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Monochrome, MemoryPrecision.OneBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(88);
                });
        }

        #endregion

        #region 8bit

        #region Gray

        [Fact(DisplayName = "Gray, 8bit, None")]
        public void CreateGrayEightBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(187);
                });
        }

        [Fact(DisplayName = "Gray, 8bit, 32bit")]
        public void CreateGrayEightBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Gray, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(220);
                });
        }

        [Fact(DisplayName = "Gray, 8bit, 64bit")]
        public void CreateGrayEightBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Gray, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(264);
                });
        }

        #endregion

        #region GrayAlpha

        [Fact(DisplayName = "GrayAlpha, 8bit, None")]
        public void CreateGrayAlphaEightBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(374);
                });
        }

        [Fact(DisplayName = "GrayAlpha, 8bit, 32bit")]
        public void CreateGrayAlphaEightBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.GrayAlpha, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(440);
                });
        }

        [Fact(DisplayName = "GrayAlpha, 8bit, 64bit")]
        public void CreateGrayAlphaEightBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.GrayAlpha, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(528);
                });
        }

        #endregion

        #region Rgb

        [Fact(DisplayName = "Rgb, 8bit, None")]
        public void CreateRgbEightBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(561);
                });
        }

        [Fact(DisplayName = "Rgb, 8bit, 32bit")]
        public void CreateRgbEightBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Rgb, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(660);
                });
        }

        [Fact(DisplayName = "Rgb, 8bit, 64bit")]
        public void CreateRgbEightBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Rgb, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(792);
                });
        }

        #endregion

        #region Rgba

        [Fact(DisplayName = "Rgba, 8bit, None")]
        public void CreateRgbaEightBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(748);
                });
        }

        [Fact(DisplayName = "Rgba, 8bit, 32bit")]
        public void CreateRgbaEightBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Rgba, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(880);
                });
        }

        [Fact(DisplayName = "Rgb, 8bit, 64bit")]
        public void CreateRgbaEightBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Rgba, MemoryPrecision.EightBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1056);
                });
        }

        #endregion

        #endregion

        #region 16bit

        #region Gray

        [Fact(DisplayName = "Gray, 16bit, None")]
        public void CreateGraySixteenBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Gray, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(374);
                });
        }

        [Fact(DisplayName = "Gray, 16bit, 32bit")]
        public void CreateGraySixteenBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Gray, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(396);
                });
        }

        [Fact(DisplayName = "Gray, 16bit, 64bit")]
        public void CreateGraySixteenBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Gray, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(440);
                });
        }

        #endregion

        #region GrayAlpha

        [Fact(DisplayName = "GrayAlpha, 16bit, None")]
        public void CreateGrayAlphaSixteenBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(748);
                });
        }

        [Fact(DisplayName = "GrayAlpha, 16bit, 32bit")]
        public void CreateGrayAlphaSixteenBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(792);
                });
        }

        [Fact(DisplayName = "GrayAlpha, 16bit, 64bit")]
        public void CreateGrayAlphaSixteenBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.GrayAlpha, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(880);
                });
        }

        #endregion

        #region Rgb

        [Fact(DisplayName = "Rgb, 16bit, None")]
        public void CreateRgbSixteenBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Rgb, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1122);
                });
        }

        [Fact(DisplayName = "Rgb, 16bit, 32bit")]
        public void CreateRgbSixteenBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Rgb, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1188);
                });
        }

        [Fact(DisplayName = "Rgb, 16bit, 64bit")]
        public void CreateRgbSixteenBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Rgb, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1320);
                });
        }

        #endregion

        #region Rgba

        [Fact(DisplayName = "Rgba, 16bit, None")]
        public void CreateRgbaSixteenBitNoneBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.None, ColorChannels.Rgba, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1496);
                });
        }

        [Fact(DisplayName = "Rgba, 16bit, 32bit")]
        public void CreateRgbaSixteenBit32bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At32Bit, ColorChannels.Rgba, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1584);
                });
        }

        [Fact(DisplayName = "Rgb, 16bit, 64bit")]
        public void CreateRgbaSixteenBit64bitBufferSuccessfully()
        {
            Given()
            .When(() => new ImageMemory((17, 11), MemoryAlignment.At64Bit, ColorChannels.Rgba, MemoryPrecision.SixteenBit))
            .Then(memory =>
                {
                    memory.Size.Should().Be(1760);
                });
        }

        #endregion

        #endregion
    }
}