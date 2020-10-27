using HelloCode.Environment.Analyzer.CSharp.Models;
using static HelloCode.Environment.Analyzer.CSharp.Analyzers.Shared.SharedComments;

namespace HelloCode.Environment.Analyzer.CSharp.Analyzers.Shared
{
    internal abstract class SharedAnalyzer<T> where T : Solution
    {
        public SolutionAnalysis Analyze(T solution) =>
            SharedDisapproveWhenInvalid(solution) ??
            DisapproveWhenInvalid(solution) ??
            SharedApproveWhenValid(solution) ??
            ApproveWhenValid(solution) ??
            solution.ReferToMentor();

        private static SolutionAnalysis SharedDisapproveWhenInvalid(T solution)
        {
            if (solution.NoImplementationFileFound())
                return solution.ReferToMentor();

            if (solution.HasCompileErrors())
                solution.AddComment(HasCompileErrors);

            if (solution.HasMainMethod())
                solution.AddComment(HasMainMethod);

            if (solution.ThrowsNotImplementedException())
                solution.AddComment(RemoveThrowNotImplementedException);

            if (solution.WritesToConsole())
                solution.AddComment(DoNotWriteToConsole);

            return solution.HasComments
                ? solution.Disapprove()
                : solution.ContinueAnalysis();
        }

        private static SolutionAnalysis SharedApproveWhenValid(T solution) =>
            solution.ContinueAnalysis();

        protected abstract SolutionAnalysis DisapproveWhenInvalid(T solution);

        protected abstract SolutionAnalysis ApproveWhenValid(T solution);
    }
}