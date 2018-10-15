namespace CustomCode.Data.Imaging.Bmp
{
    using System.Collections.Generic;

    /// <summary>
    /// Implementation for a 24bit rgb color table that is stored in a bitmap file.
    /// </summary>
    public sealed class ColorTable : List<(byte red, byte green, byte blue)>, IColorTable
    {
        #region Logic

        /// <summary>
        /// Query if the color table contains only the colors black and white.
        /// </summary>
        /// <returns> True if the color table contains only black and white, false otherwise. </returns>
        public bool IsMonochrome()
        {
            foreach (var (red, green, blue) in this)
            {
                if ((red > 0 && red < 255) || (green > 0 && green < 255) || (blue > 0 && blue < 255))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Query if the color table contains only gray scale colors.
        /// </summary>
        /// <returns> True if the color table contains only gray scale colors, false otherwise. </returns>
        public bool IsGrayScale()
        {
            var isGrayScale = false;

            foreach (var (red, green, blue) in this)
            {
                if (red != green || red != blue || green != blue)
                {
                    return false;
                }

                if (!isGrayScale)
                {
                    isGrayScale = (red > 0 && red < 255);
                }
            }

            return isGrayScale;
        }

        #endregion
    }
}