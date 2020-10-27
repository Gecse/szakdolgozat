namespace HelloCode.Environment.Analyzer.CSharp.Models
{
    public class SolutionCommentParameter
    {
        public string Key { get; }
        public string Value { get; }

        internal SolutionCommentParameter(string key, string value) =>
            (Key, Value) = (key, value);
    }
}