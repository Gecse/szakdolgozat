using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HelloCode.Environment.Analyzer.CSharp.Analyzers.Shared
{
    internal static class SharedSyntaxFactory
    {
        public static IdentifierNameSyntax IdentifierName(ParameterSyntax parameter) =>
            SyntaxFactory.IdentifierName(parameter.Identifier);
    }
}