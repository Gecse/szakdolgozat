using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HelloCode.Environment.TestRunner.CSharp.Rewriters
{
    internal class UnskipTestsRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitAttributeArgument(AttributeArgumentSyntax node)
        {
            if (IsSkipAttributeArgument(node))
                return null;

            return base.VisitAttributeArgument(node);
        }

        private static bool IsSkipAttributeArgument(AttributeArgumentSyntax node)
        {
            return node.NameEquals?.Name.ToString() == "Skip";
        }
    }
}
