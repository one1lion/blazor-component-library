using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace One1Lion.Samples.Helpers;

public static class ColorHelpers
{
    private readonly static Regex _invalidHexCharacterPattern = new(@"[^0-9a-fA-F]");
    private readonly static int[] _validHexColorLength = new[] { 3, 6 };

    public static Color FromHexString(string hex, float alpha) => FromHexString(hex, (int)(255 * alpha));

    public static Color FromHexString(string hex, int alpha = 255)
    {
        var useAlpha = Math.Clamp(alpha, 0, 255);
        hex = hex.Replace("#", "").Trim();
        if (!hex.IsValidHexColorString())
        {
            return Color.FromArgb(useAlpha, Color.Black);
        }

        switch (hex.Length)
        {
            case 3:
                {
                    // #fff == #ffffff
                    var r = int.Parse($"{hex[0]}{hex[0]}", NumberStyles.HexNumber);
                    var g = int.Parse($"{hex[1]}{hex[1]}", NumberStyles.HexNumber);
                    var b = int.Parse($"{hex[2]}{hex[2]}", NumberStyles.HexNumber);

                    return Color.FromArgb(useAlpha, r, g, b);
                }
            case 6:
                {
                    // #f5f5f5
                    var r = int.Parse($"{hex[0]}{hex[1]}", NumberStyles.HexNumber);
                    var g = int.Parse($"{hex[2]}{hex[3]}", NumberStyles.HexNumber);
                    var b = int.Parse($"{hex[4]}{hex[5]}", NumberStyles.HexNumber);

                    return Color.FromArgb(useAlpha, r, g, b);
                }
        }

        return Color.FromArgb(useAlpha, Color.Black);
    }

    public static bool IsValidHexColorString(this string hexString) =>
         _validHexColorLength.Contains(hexString.Length) && !_invalidHexCharacterPattern.IsMatch(hexString);
}
