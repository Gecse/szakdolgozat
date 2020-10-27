using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HelloCode.Environment.Analyzer.CSharp.Syntax.Rewriters
{
    internal class NormalizeSyntaxRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode Visit(SyntaxNode node)
        {
            if (node == null)
                return null;

            var normalizedNode = node.WithoutTrivia().NormalizeWhitespace();
            return base.Visit(normalizedNode);
        }
    }
}