using System.Text;

namespace GameJamToolkit.Utils
{
    public static class StringExtensions
    {
        public static string Truncate(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length <= maxLength) return s;
            return s.Substring(0, maxLength) + "...";
        }

        public static string Repeat(this string s, int count)
        {
            if (string.IsNullOrEmpty(s) || count <= 0) return string.Empty;
            var sb = new StringBuilder(s.Length * count);
            for (int i = 0; i < count; i++) sb.Append(s); // R2: bounded by count
            return sb.ToString();
        }
    }
}
