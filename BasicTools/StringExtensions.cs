namespace BasicTools
{
    public static class StringExtensions
    {
        public static string ExtractBetween(this string source, char start, char end)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            int startIdx = source.IndexOf(start);
            if (startIdx == -1) return string.Empty;

            int endIdx = source.IndexOf(end, startIdx + 1);
            if (endIdx == -1) return string.Empty;

            return source.Substring(startIdx + 1, endIdx - startIdx - 1);
        }
    }
}
