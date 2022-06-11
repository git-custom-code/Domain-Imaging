namespace CustomCode.Domain.Imaging.Memory;

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

    /// <inheritdoc cref="IComparable" />
    public int CompareTo(object? other)
    {
        if (other is Bit bit)
        {
            return CompareTo(bit);
        }

        throw new ArgumentException($"Other is not of type {nameof(Bit)}", nameof(other));
    }

    /// <inheritdoc cref="IComparable{T}" />
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

    /// <inheritdoc cref="object" />
    public override bool Equals(object? obj)
    {
        if (obj is Bit bit)
        {
            return Equals(bit);
        }

        return false;
    }

    /// <inheritdoc cref="IEquatable{T}" />
    public bool Equals(Bit other)
    {
        return Value == other.Value;
    }

    /// <inheritdoc cref="object" />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <inheritdoc cref="IConvertible" />
    public TypeCode GetTypeCode()
    {
        return TypeCode.Object;
    }

    /// <inheritdoc cref="IConvertible" />
    public bool ToBoolean(IFormatProvider? provider)
    {
        return Value == 1;
    }

    /// <inheritdoc cref="IConvertible" />
    public byte ToByte(IFormatProvider? provider)
    {
        return Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public char ToChar(IFormatProvider? provider)
    {
        return (char)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new InvalidCastException($"Cannot cast from {nameof(Bit)} to {nameof(DateTime)}");
    }

    /// <inheritdoc cref="IConvertible" />
    public decimal ToDecimal(IFormatProvider? provider)
    {
        return (decimal)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public double ToDouble(IFormatProvider? provider)
    {
        return (double)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public short ToInt16(IFormatProvider? provider)
    {
        return (short)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public int ToInt32(IFormatProvider? provider)
    {
        return (int)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public long ToInt64(IFormatProvider? provider)
    {
        return (long)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public sbyte ToSByte(IFormatProvider? provider)
    {
        return (sbyte)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public float ToSingle(IFormatProvider? provider)
    {
        return (float)Value;
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        return $"{Value}";
    }

    /// <inheritdoc cref="IConvertible" />
    public string ToString(IFormatProvider? provider)
    {
        return $"{Value}";
    }

    /// <inheritdoc cref="IConvertible" />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (string.IsNullOrEmpty(format))
        {
            return ToString(formatProvider);
        }
        return string.Format(formatProvider, format, Value);
    }

    /// <inheritdoc cref="IConvertible" />
    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        return Convert.ChangeType(Value, conversionType, provider);
    }

    /// <inheritdoc cref="IConvertible" />
    public ushort ToUInt16(IFormatProvider? provider)
    {
        return (ushort)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public uint ToUInt32(IFormatProvider? provider)
    {
        return (uint)Value;
    }

    /// <inheritdoc cref="IConvertible" />
    public ulong ToUInt64(IFormatProvider? provider)
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
        return value.Value == 1;
    }

    #endregion
}
