using HelloCode.Environment.Analyzer.CSharp.Syntax.Comparers;
using Microsoft.CodeAnalysis;

namespace HelloCode.Environment.Analyzer.CSharp.Syntax.Extensions
{
    internal static class SyntaxNodeComparisonExtensions
    {
        public static SyntaxNode Simplify(this SyntaxNode node) =>
            SyntaxNodeSimplifier.Simplify(node);

        public static bool IsEquivalentWhenNormalized(this SyntaxNode node, SyntaxNode other) =>
            SyntaxNodeComparer.IsEquivalentToNormalized(node, other);
    }
}