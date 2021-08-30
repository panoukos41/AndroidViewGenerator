using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace P41.AndroidViewGenerator;

[Generator]
internal class AndroidViewGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new GenerateViewFinder());
#if DEBUG
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            //System.Diagnostics.Debugger.Launch();
        }
#endif
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;
        var files = context.AdditionalFiles.GetXmlFiles();
        var views = ((GenerateViewFinder)context.SyntaxReceiver!).Views;

        foreach (var view in views)
        {
            var model = compilation.GetSemanticModel(view.SyntaxTree);
            var symbol = model.GetDeclaredSymbol(view)!;

            if (!ShouldProcess(symbol)) continue;

            var @namespace = symbol.GetNamespace();
            var @class = symbol.GetClass();
            var fileName = GetFileName(model, view);

            if (files.ContainsKey(fileName))
            {
                ProcessFile(files[fileName], context, @namespace, @class, symbol.IsActivity());
            }
        }
    }

    private static string GetFileName(SemanticModel model, ClassDeclarationSyntax view)
    {
        var attribute = view.AttributeLists
            .SelectMany(x => x.Attributes)
            .First(x => Templating.AttribugeNames.Contains(x.Name.ToString()));

        var fileArg = attribute.ArgumentList!.Arguments[0];
        var fileExp = fileArg.Expression;
        return model.GetConstantValue(fileExp).ToString();
    }

    private static bool ShouldProcess(INamedTypeSymbol symbol)
    {
        if (!symbol.ContainsAttribute()) return false;

        return symbol.IsFragment() || symbol.IsActivity();
    }

    private static void ProcessFile(AdditionalText file, GeneratorExecutionContext context, string @namespace, string @class, bool isActivity)
    {
        const string xmlNamespace = "http://schemas.android.com/apk/res/android";
        const string xmlLocalName = "id";

        var androidId = XName.Get(xmlLocalName, xmlNamespace);

        var text = file.GetText(context.CancellationToken)?.ToString();

        if (string.IsNullOrWhiteSpace(text)) return;

        var doc = XDocument.Parse(text);

        var props = doc
            .Descendants()
            .Where(x => x.Attributes(androidId).Any())
            .Select(element =>
            {
                string elementName = element.Name.LocalName;
                string id = element.Attribute(androidId)!.Value.Substring(5);

                return (elementName, id);
            });

        var sb = new StringBuilder().Render(@namespace, @class, props, isActivity);

        context.AddSource($"{@class}.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }
}
