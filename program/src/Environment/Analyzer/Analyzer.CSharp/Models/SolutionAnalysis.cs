namespace HelloCode.Environment.Analyzer.CSharp.Models
{
    public class SolutionAnalysis
    {
        public SolutionStatus Status { get; }
        public SolutionComment[] Comments { get; }

        internal SolutionAnalysis(SolutionStatus status, SolutionComment[] comments) =>
            (Status, Comments) = (status, comments);
    }
}