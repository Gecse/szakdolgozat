namespace HelloCode.Environment.TestRunner.CSharp.Extensions
{
    internal static class StringExtensions
    {
        public static string NormalizeNewlines(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = input.Replace("\r\n", "\n")
                    .Replace("\r", string.Empty)
                    .Trim();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            return input;
        }
    }
}
