namespace CustomCode.Domain.Imaging.Memory
{
    using System;

    /// <summary>
    /// Value type that represents a single bit.
    /// </summary>
    public struct Bit : IComparable, IComparable<Bit>, IConvertible, IEquatable<Bit>, IFormattable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Bit"/> type.
        /// </summary>
        /// <param name="value"> The </param>
        public Bit(bool value)
        {
            Value = value ? (byte)1 : (byte)0;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the bit's internal value.
        /// </summary>
        private byte Value { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that
        /// indicates whether the current instance precedes, follows, or occurs in the same position
        /// in the sort order as the other object.
        /// </summary>
        /// <param name="other"> An object to compare with this instance. </param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// Less than zero:    This instance precedes other in the sort order.
        /// Zero:              This instance occurs in the same position in the sort order as other.
        /// Greater than zero: This instance follows other in the sort order.
        /// </returns>
        public int CompareTo(object other)
        {
            if (other is Bit bit)
            {
                return CompareTo(bit);
            }

            throw new ArgumentException($"Other is not of type {nameof(Bit)}", nameof(other));
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that
        /// indicates whether the current instance precedes, follows, or occurs in the same position
        /// in the sort order as the other object.
        /// </summary>
        /// <param name="other"> An object to compare with this instance. </param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// Less than zero:    This instance precedes other in the sort order.
        /// Zero:              This instance occurs in the same position in the sort order as other.
        /// Greater than zero: This instance follows other in the sort order.
        /// </returns>
        public int CompareTo(Bit other)
        {
            if (other.Value == Value)
            {
                return 0;
            }

            if (other.Value < Value)
            {
                return 1;
            }

            return -1;
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
            if (obj is Bit bit)
            {
                return Equals(bit);
            }

            return false;
        }

        /// <summary>
        /// Indicates whether this instance and a specified other <see cref="Bit"/> are equal.
        /// </summary>
        /// <param name="other"> The <see cref="Bit"/> to compare with the current instance. </param>
        /// <returns>
        /// True if <paramref name="other"/> <see cref="Bit"/> and this instance are the same type and
        /// represent the same value, false otherwise.
        /// </returns>
        public bool Equals(Bit other)
        {
            return Value == other.Value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns> A hash code for the current <see cref="Bit"/>. </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns the <see cref="TypeCode"/> for this instance.
        /// </summary>
        /// <returns>
        /// The enumerated constant that is the <see cref="TypeCode"/> of the class or value type
        /// that implements this interface.
        /// </returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="bool"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="bool"/> value equivalent to the value of this instance. </returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            return Value == 1;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="byte"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="byte"/> value equivalent to the value of this instance. </returns>
        public byte ToByte(IFormatProvider provider)
        {
            return Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="char"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="char"/> value equivalent to the value of this instance. </returns>
        public char ToChar(IFormatProvider provider)
        {
            return (char)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="DateTime"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="DateTime"/> value equivalent to the value of this instance. </returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException($"Cannot cast from {nameof(Bit)} to {nameof(DateTime)}");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="decimal"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="decimal"/> value equivalent to the value of this instance. </returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            return (decimal)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="double"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="double"/> value equivalent to the value of this instance. </returns>
        public double ToDouble(IFormatProvider provider)
        {
            return (double)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="short"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="short"/> value equivalent to the value of this instance. </returns>
        public short ToInt16(IFormatProvider provider)
        {
            return (short)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="int"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="int"/> value equivalent to the value of this instance. </returns>
        public int ToInt32(IFormatProvider provider)
        {
            return (int)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="long"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="long"/> value equivalent to the value of this instance. </returns>
        public long ToInt64(IFormatProvider provider)
        {
            return (long)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="sbyte"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="sbyte"/> value equivalent to the value of this instance. </returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="float"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="float"/> value equivalent to the value of this instance. </returns>
        public float ToSingle(IFormatProvider provider)
        {
            return (float)Value;
        }

        /// <summary>
        /// Creates a human readable string representation of this instance.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>

        public override string ToString()
        {
            return $"{Value}";
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="string"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="string"/> value equivalent to the value of this instance. </returns>
        public string ToString(IFormatProvider provider)
        {
            return $"{Value}";
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="format">
        /// The format to use or a null reference to use the default format defined for the type
        /// of the <see cref="IFormattable"/> implementation.
        /// </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value or a null reference to obtain the numeric format
        /// information from the current locale setting of the operating system.
        /// </param>
        /// <returns> The value of the current instance in the specified format. </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, format, Value);
        }

        /// <summary>
        /// Converts the value of this instance to an <see cref="object"/> of the specified <see cref="Type"/>
        /// that has an equivalent value, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType">
        /// The <see cref="Type"/> to which the value of this instance is converted.
        /// </param>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// An <see cref="object"/> instance of type <paramref name="conversionType"/> whose value
        /// is equivalent to the value of this instance.
        /// </returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType, provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="ushort"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="ushort"/> value equivalent to the value of this instance. </returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="uint"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="uint"/> value equivalent to the value of this instance. </returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            return (uint)Value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="ulong"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
        /// </param>
        /// <returns> A <see cref="ulong"/> value equivalent to the value of this instance. </returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            return (ulong)Value;
        }

        /// <summary>
        /// Compare two <see cref="Bit"/>s for equality.
        /// </summary>
        /// <param name="left"> The left-hand side argument. </param>
        /// <param name="right"> The right-hand side argument. </param>
        /// <returns> True if both bits are equal, false otherwise. </returns>
        public static bool operator ==(Bit left, Bit right)
        {
            return left.Value == right.Value;
        }

        /// <summary>
        /// Compare two <see cref="Bit"/>s for inequality.
        /// </summary>
        /// <param name="left"> The left-hand side argument. </param>
        /// <param name="right"> The right-hand side argument. </param>
        /// <returns> False if both bits are equal, true otherwise. </returns>
        public static bool operator !=(Bit left, Bit right)
        {
            return left.Value != right.Value;
        }

        /// <summary>
        /// Convert a <see cref="bool"/> to a <see cref="Bit"/>.
        /// </summary>
        /// <param name="value"> The <see cref="bool"/> to be converted. </param>
        public static implicit operator Bit(bool value)
        {
            return new Bit(value);
        }

        /// <summary>
        /// Convert a <see cref="Bit"/> to a <see cref="bool"/>.
        /// </summary>
        /// <param name="value"> The <see cref="Bit"/> to be converted. </param>
        public static implicit operator bool(Bit value)
        {
            return (value.Value == 1);
        }

        #endregion
    }
}