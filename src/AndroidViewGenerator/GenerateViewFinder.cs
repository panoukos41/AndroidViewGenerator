using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace P41.AndroidViewGenerator;

internal class GenerateViewFinder : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> Views { get; } = new List<ClassDeclarationSyntax>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not ClassDeclarationSyntax classSyntax) return;

        var attrs = classSyntax
            .DescendantNodes()
            .OfType<AttributeSyntax>()
            .Any(attr =>
            {
                var name = attr.Name.ToString();

                return Templating.AttribugeNames.Contains(name);
            });

        if (attrs) Views.Add(classSyntax);
    }
}