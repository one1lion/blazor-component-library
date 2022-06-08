using System.Drawing;
using System.Linq;

namespace One1Lion.Samples.Helpers;

public static class ColorHelpers
{
    public static Color FromHex(string hex, float alpha = 1.0f)
    {
        hex = hex.Replace("#", "");
        var useAlpha = (int)(100 * alpha);

        // TODO: finish the logic to cast from hex to decimal for each color
        switch(hex.Length)
        {
            case 3:
                var colArr = hex.Split();
                var r = $"{colArr[0]}{colArr[0]}";
                return Color.FromArgb(useAlpha, Color.Black);
            case 6:

                return Color.FromArgb(useAlpha, Color.Black);
            default:
                return Color.FromArgb(useAlpha, Color.Black);
        }
    }  
}
