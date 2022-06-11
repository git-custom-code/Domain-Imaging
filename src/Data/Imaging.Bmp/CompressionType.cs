namespace CustomCode.Data.Imaging.Bmp;

public enum CompressionType : uint
{
    Rgb = 0,
    Rle8 = 1,
    Rle4 = 2,
    Bitfields =3,
    Huffman1D = Bitfields,
    Jpeg = 4,
    Rle24 = Jpeg,
    Png = 5,
    AlphaBitfields = 6,
    Cmyk = 11,
    CmykRle8 = 12,
    CmykRle4 = 13
}
