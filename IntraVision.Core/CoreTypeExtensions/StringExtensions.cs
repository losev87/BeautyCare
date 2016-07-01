namespace IntraVision.Core
{
    public static class StringExtensions
    {
        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
