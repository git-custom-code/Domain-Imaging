namespace CustomCode.Domain.Imaging.Tests
{
    using Xunit;
    public sealed class DimensionTests
    {
        [Fact(DisplayName = "Dimension from constant width and height")]
        public void CreateDimensionFromConstantWidthAndHeight()
        {
            // Given

            // When
            var dimension = new Dimension(1u, 2u);

            // Then
            Assert.Equal(1u, dimension.Width);
            Assert.Equal(2u, dimension.Height);
        }

        [Fact(DisplayName = "Dimension from tuple")]
        public void CreateDimensionFromTuple()
        {
            // Given
            var tuple = (1u, 2u);
            
            // When
            var dimension = new Dimension(tuple);

            // Then
            Assert.Equal(1u, dimension.Width);
            Assert.Equal(2u, dimension.Height);
        }

        [Fact(DisplayName = "Dimension from other dimension")]
        public void CreateDimensionFromOtherDimension()
        {
            // Given
            var other = new Dimension(1u, 2u);

            // When
            var dimension = new Dimension(other);

            // Then
            Assert.Equal(1u, dimension.Width);
            Assert.Equal(2u, dimension.Height);
        }

        [Fact(DisplayName = "Deconstruct dimension")]
        public void DeconstructDimension()
        {
            // Given
            var dimension = new Dimension(1u, 2u);

            // When
            var (width, height) = dimension;

            // Then
            Assert.Equal(1u, width);
            Assert.Equal(2u, height);
        }

        [Fact(DisplayName = "Assign tuple to dimension")]
        public void AssignTupleToDimension()
        {
            // Given
            Dimension dimension;
            var tuple = (1u, 2u);

            // When
            dimension = tuple;

            // Then
            Assert.Equal(1u, dimension.Width);
            Assert.Equal(2u, dimension.Height);
        }

        [Fact(DisplayName = "Compare two dimensions for equality")]
        public void CompareTwoDimensionsForEquality()
        {
            // Given
            var left = new Dimension(1u, 2u);
            var right = new Dimension(3u, 4u);

            // When
            var areEqual = left == right;

            // Then
            Assert.False(areEqual);
        }

        [Fact(DisplayName = "Compare two dimensions for inequality")]
        public void CompareTwoDimensionsForInequality()
        {
            // Given
            var left = new Dimension(1u, 2u);
            var right = new Dimension(3u, 4u);

            // When
            var areInequal = left != right;

            // Then
            Assert.True(areInequal);
        }
    }
}