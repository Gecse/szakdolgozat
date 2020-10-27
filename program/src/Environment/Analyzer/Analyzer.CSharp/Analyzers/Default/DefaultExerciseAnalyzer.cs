using HelloCode.Environment.Analyzer.CSharp.Analyzers.Shared;
using HelloCode.Environment.Analyzer.CSharp.Models;

namespace HelloCode.Environment.Analyzer.CSharp.Analyzers.Default
{
    internal class DefaultExerciseAnalyzer : SharedAnalyzer<DefaultSolution>
    {
        protected override SolutionAnalysis DisapproveWhenInvalid(DefaultSolution solution) =>
            solution.ContinueAnalysis();

        protected override SolutionAnalysis ApproveWhenValid(DefaultSolution solution) =>
            solution.ContinueAnalysis();
    }
}