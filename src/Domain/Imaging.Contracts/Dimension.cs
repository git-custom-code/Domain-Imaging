namespace CustomCode.Domain.Imaging
{
    /// <summary>
    /// A type that contains an image's width and height in pixel.
    /// </summary>
    public readonly struct Dimension
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Dimension"/> type.
        /// </summary>
        /// <param name="width"> The dimension's width [in pixel]. </param>
        /// <param name="height"> The dimension's height [in pixel]. </param>
        public Dimension(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Dimension"/> type.
        /// </summary>
        /// <param name="dimension"> The dimensions width and height [in pixel]. </param>
        public Dimension((uint width, uint height) dimension)
        {
            Width = dimension.width;
            Height = dimension.height;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Dimension"/> type.
        /// </summary>
        /// <param name="dimension"> The dimension to be copied. </param>
        public Dimension(Dimension dimension)
        {
            Width = dimension.Width;
            Height = dimension.Height;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the dimension's height [in pixel].
        /// </summary>
        public uint Height { get; }

        /// <summary>
        /// Gets the dimension's width [in pixel].
        /// </summary>
        public uint Width { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Deconstructs this instance to a <see cref="System.Tuple{T1, T2}"/>.
        /// </summary>
        /// <param name="width"> The dimension's width [in pixel]. </param>
        /// <param name="height"> The dimension's height [in pixel]. </param>
        public void Deconstruct(out uint width, out uint height)
        {
            width = Width;
            height = Height;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is Dimension dimension)
            {
                return Width == dimension.Width && Height == dimension.Height;
            }

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + (int)Width;
            hash = hash * 31 + (int)Height;
            return hash;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }

        /// <summary>
        /// Convert a value tuple to a <see cref="Dimension"/>.
        /// </summary>
        /// <param name="tuple"> The tuple to be converted.</param>
        public static implicit operator Dimension((uint width, uint height) tuple)
        {
            return new Dimension(tuple);
        }

        /// <summary>
        /// Compare two <see cref="Dimension"/>s for equality.
        /// </summary>
        /// <param name="left"> The left-hand side argument. </param>
        /// <param name="right"> The right-hand side argument. </param>
        /// <returns> True if both dimensions are equal, false otherwise. </returns>
        public static bool operator ==(Dimension left, Dimension right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        /// <summary>
        /// Compare two <see cref="Dimension"/>s for inequality.
        /// </summary>
        /// <param name="left"> The left-hand side argument. </param>
        /// <param name="right"> The right-hand side argument. </param>
        /// <returns> False if both dimensions are equal, true otherwise. </returns>
        public static bool operator !=(Dimension left, Dimension right)
        {
            return left.Width != right.Width || left.Height != right.Height;
        }

        #endregion
    }
}