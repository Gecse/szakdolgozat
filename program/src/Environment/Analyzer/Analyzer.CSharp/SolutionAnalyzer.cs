using HelloCode.Environment.Analyzer.CSharp.Analyzers.Default;
using HelloCode.Environment.Analyzer.CSharp.Models;

namespace HelloCode.Environment.Analyzer.CSharp
{
    internal static class SolutionAnalyzer
    {
        public static SolutionAnalysis Analyze(Solution solution)
        {
            return solution.Slug switch
            {
                _ => new DefaultExerciseAnalyzer().Analyze(new DefaultSolution(solution)),
            };
        }
    }
}