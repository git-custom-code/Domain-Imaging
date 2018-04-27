namespace CustomCode.Domain.Imaging
{
    /// <summary>
    /// A type that contains an image's width and height in pixel.
    /// </summary>
    public struct Dimension
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Dimension"/> type.
        /// </summary>
        /// <param name="width"> The dimension's width in pixel. </param>
        /// <param name="height"> The dimension's height in pixel. </param>
        public Dimension(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Dimension"/> type.
        /// </summary>
        /// <param name="dimension"> The dimensions width and height in pixel. </param>
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
        /// Gets the dimension's height in pixel.
        /// </summary>
        public uint Height { get; }

        /// <summary>
        /// Gets the dimension's width in pixel.
        /// </summary>
        public uint Width { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns> A 32-bit signed integer that is the hash code for this instance. </returns>
        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + (int)Width;
            hash = hash * 31 + (int)Height;
            return hash;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj"> The object to compare with the current instance. </param>
        /// <returns>
        /// True if <paramref name="obj"/> and this instance are the same type and represent the same value, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Dimension dimension)
            {
                return (Width == dimension.Width && Height == dimension.Height);
            }

            return false;
        }

        /// <summary>
        /// Creates a human readable string representation of this instance.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>
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
        /// Convert a <see cref="Dimension"/> to a value tuple.
        /// </summary>
        /// <param name="dimension"> The dimension to be converted. </param>
        public static explicit operator (uint width, uint height)(Dimension dimension)
        {
            return (width: dimension.Width, height: dimension.Height);
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