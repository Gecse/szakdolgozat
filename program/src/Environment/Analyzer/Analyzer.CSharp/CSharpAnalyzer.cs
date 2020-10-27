using HelloCode.Environment.Analyzer.CSharp.Models;

namespace HelloCode.Environment.Analyzer.CSharp
{
    public class CSharpAnalyzer
    {
        public SolutionAnalysis Run(Options options)
        {
            var solution = SolutionParser.Parse(options);
            return SolutionAnalyzer.Analyze(solution);
            // SolutionAnalysisWriter.WriteToFile(options, solutionAnalysis);
        }
    }
}