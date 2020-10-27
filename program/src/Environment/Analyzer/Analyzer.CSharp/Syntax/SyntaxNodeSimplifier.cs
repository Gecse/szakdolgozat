using System.Linq;
using HelloCode.Environment.Analyzer.CSharp.Syntax.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HelloCode.Environment.Analyzer.CSharp.Syntax
{
    internal static class SyntaxNodeSimplifier
    {
        private static readonly CSharpSyntaxRewriter[] SyntaxRewriters =
        {
            new SimplifyFullyQualifiedNameSyntaxRewriter(),
            new UseBuiltInKeywordSyntaxRewriter(),
            new InvertNegativeConditionalSyntaxRewriter(),
            new AddBracesSyntaxRewriter(),
            new ExponentNotationSyntaxRewriter()
        };

        public static SyntaxNode Simplify(SyntaxNode reducedSyntaxRoot) =>
            SyntaxRewriters.Aggregate(reducedSyntaxRoot, (acc, rewriter) => rewriter.Visit(acc));
    }
}