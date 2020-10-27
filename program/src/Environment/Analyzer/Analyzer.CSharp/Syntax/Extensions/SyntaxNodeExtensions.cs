using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HelloCode.Environment.Analyzer.CSharp.Syntax.Extensions
{
    internal static class SyntaxNodeExtensions
    {
        public static IEnumerable<TSyntaxNode> DescendantNodes<TSyntaxNode>(this SyntaxNode syntaxNode)
            where TSyntaxNode : SyntaxNode =>
            syntaxNode?.DescendantNodes().OfType<TSyntaxNode>() ?? Enumerable.Empty<TSyntaxNode>();

        public static MethodDeclarationSyntax GetClassMethod(this SyntaxNode syntaxNode, string className,
            string methodName) => syntaxNode.GetClass(className).GetMethod(methodName);

        public static ClassDeclarationSyntax GetClass(this SyntaxNode syntaxNode, string className) =>
            syntaxNode?
                .DescendantNodes<ClassDeclarationSyntax>()
                .FirstOrDefault(syntax => syntax.Identifier.Text == className);

        public static MethodDeclarationSyntax GetMethod(this SyntaxNode syntaxNode, string methodName) =>
            syntaxNode?
                .DescendantNodes<MethodDeclarationSyntax>()
                .FirstOrDefault(syntax => syntax.Identifier.Text == methodName);

        public static bool ThrowsExceptionOfType<TException>(this SyntaxNode syntaxNode) where TException : Exception =>
            syntaxNode?
                .DescendantNodes<ThrowStatementSyntax>()
                .Any(throwsStatement =>
                        throwsStatement.Expression is ObjectCreationExpressionSyntax objectCreationExpression &&
                        objectCreationExpression.Type.IsEquivalentWhenNormalized(
                            SyntaxFactory.IdentifierName(typeof(TException).Name))) ?? false;

        public static bool InvokesMethod(this SyntaxNode syntaxNode, string identifier) =>
            syntaxNode?
                .DescendantNodes<InvocationExpressionSyntax>()
                .Any(invocationExpression => invocationExpression.InvokesMethod(identifier)) ?? false;

        private static bool InvokesMethod(this InvocationExpressionSyntax invocationExpression, string identifier) =>
            invocationExpression?.Expression.WithoutTrivia().ToFullString() == identifier;
    }
}