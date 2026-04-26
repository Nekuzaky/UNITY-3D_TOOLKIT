using UnityEngine;

namespace GameJamToolkit.Utils
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color c, float a) { c.a = a; return c; }

        public static Color FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex)) return Color.white;
            if (hex[0] == '#') hex = hex.Substring(1);
            if (ColorUtility.TryParseHtmlString("#" + hex, out var result)) return result;
            return Color.white;
        }
    }
}
