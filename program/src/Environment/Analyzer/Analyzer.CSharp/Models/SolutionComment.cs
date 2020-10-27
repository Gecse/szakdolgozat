namespace HelloCode.Environment.Analyzer.CSharp.Models
{
    public class SolutionComment
    {
        public string Comment { get; }
        public SolutionCommentParameter[] Parameters { get; }

        internal SolutionComment(string comment, params SolutionCommentParameter[] parameters) =>
            (Comment, Parameters) = (comment, parameters);
    }
}