using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace HelloCode.Environment.TestRunner.CSharp.Rewriters
{
    internal class CaptureTracesAsTestOutputRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            if (node.DescendantNodes().Any(IsTestClass))
                return base.VisitCompilationUnit(
                    node.WithUsings(
                        node.Usings.Add(
                            UsingDirective(QualifiedName(
                            IdentifierName("Xunit").WithLeadingTrivia(Space),
                            IdentifierName("Abstractions"))))));

            return base.VisitCompilationUnit(node);
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (IsTestClass(node))
                return base.VisitClassDeclaration(
                    node.WithBaseList(
                            BaseList(
                                SingletonSeparatedList<BaseTypeSyntax>(
                                    SimpleBaseType(
                                        IdentifierName("TestBase")))))
                        .WithMembers(node.Members.Add(ConstructorDeclaration(
                                Identifier(node.Identifier.Text))
                            .WithModifiers(
                                TokenList(
                                    Token(SyntaxKind.PublicKeyword).WithTrailingTrivia(Space)))
                            .WithParameterList(
                                ParameterList(
                                    SingletonSeparatedList<ParameterSyntax>(
                                        Parameter(
                                                Identifier("output"))
                                            .WithType(
                                                IdentifierName("ITestOutputHelper").WithTrailingTrivia(Space)))))
                            .WithInitializer(
                                ConstructorInitializer(
                                    SyntaxKind.BaseConstructorInitializer,
                                    ArgumentList(
                                        SingletonSeparatedList<ArgumentSyntax>(
                                            Argument(
                                                IdentifierName("output"))))))
                            .WithBody(
                                Block()))));

            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression is MemberAccessExpressionSyntax memberAccessExpression)
            {
                var memberAccess = memberAccessExpression.WithoutTrivia().ToFullString();

                if (memberAccess.EndsWith("Console.Write") ||
                    memberAccess.EndsWith("Console.Error.Write") ||
                    memberAccess.EndsWith("Console.Out.Write"))
                    return node.WithExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("System"),
                                    IdentifierName("Diagnostics")),
                                IdentifierName("Trace")),
                            IdentifierName("Write")));

                if (memberAccess.EndsWith("Console.WriteLine") ||
                    memberAccess.EndsWith("Console.Error.WriteLine") ||
                    memberAccess.EndsWith("Console.Out.WriteLine"))
                    return node.WithExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("System"),
                                    IdentifierName("Diagnostics")),
                                IdentifierName("Trace")),
                            IdentifierName("WriteLine")));
            }

            return base.VisitInvocationExpression(node);
        }

        private static bool IsTestClass(SyntaxNode descendant)
        {
            return descendant is ClassDeclarationSyntax classDeclaration &&
                classDeclaration.Identifier.Text.EndsWith("Test");
        }
    }
}
