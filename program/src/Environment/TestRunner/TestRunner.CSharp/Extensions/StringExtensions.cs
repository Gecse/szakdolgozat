namespace HelloCode.Environment.TestRunner.CSharp.Extensions
{
    internal static class StringExtensions
    {
        internal static string NormalizeNewlines(this string str)
        {
            if (str == null || str == string.Empty)
                return null;

            str = str.Replace("\r\n", "\n")
                    .Replace("\r", string.Empty)
                    .Trim();

            if (string.IsNullOrWhiteSpace(str))
                return null;

            return str;
        }
    }
}
