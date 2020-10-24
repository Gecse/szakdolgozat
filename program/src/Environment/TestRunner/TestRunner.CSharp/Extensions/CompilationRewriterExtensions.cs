using HelloCode.Environment.TestRunner.CSharp.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HelloCode.Environment.TestRunner.CSharp.Extensions
{
    internal static class CompilationRewriterExtensions
    {
        public static Compilation Rewrite(this Compilation compilation)
        {
            return compilation
                .UnskipTests()
                .CaptureTracesAsTestOutput();
        }

        private static Compilation UnskipTests(this Compilation compilation)
        {
            return compilation.Rewrite(new UnskipTestsRewriter());
        }

        private static Compilation CaptureTracesAsTestOutput(this Compilation compilation)
        {
            return compilation.Rewrite(new CaptureTracesAsTestOutputRewriter());
        }

        private static Compilation Rewrite(this Compilation compilation, CSharpSyntaxRewriter rewriter)
        {
            foreach (var syntaxTree in compilation.SyntaxTrees)
                compilation = compilation.ReplaceSyntaxTree(
                    syntaxTree,
                    syntaxTree.WithRootAndOptions(
                        rewriter.Visit(syntaxTree.GetRoot()), syntaxTree.Options));

            return compilation;
        }
    }
}
