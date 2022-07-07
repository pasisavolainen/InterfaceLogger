using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using InterfaceLogger.AOT.Utility;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace InterfaceLogger.AOT
{
    [Generator]
    public class LoggerGenerator : IIncrementalGenerator
    {

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classDeclarations = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: NewMethod,
                transform: GetTypeSymbols).Collect();
            context.RegisterSourceOutput(classDeclarations, GenerateSource);
        }

        private bool NewMethod(SyntaxNode node, CancellationToken ct)
        {
            return node is ClassDeclarationSyntax;
        }

        private void GenerateSource(SourceProductionContext context, ImmutableArray<ITypeSymbol> typeSymbols)
        {
            var sb = new StringBuilder();
            const string factorySignature = "global::InterfaceLogger.Interfaces.ILoggerFactory";
            foreach (var symbol in typeSymbols)
            {
                if (symbol is null)
                    continue;

                if (!symbol.AllInterfaces.Any(i => i.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == factorySignature))
                    continue;

                var methods = symbol.GetMembers()
                                    .Select(m => m as IMethodSymbol)
                                    .Where(m => m?.IsPartialDefinition ?? false).ToArray();

                if (methods.Length == 0)
                    continue;

                InitiateFactory(symbol, sb);

                foreach(var method in methods)
                {
                    GenerateLogger(method, sb);
                }
                FinishFactory(symbol, sb);

                //sb.AppendLine("// " + symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            }

            context.AddSource($"all_typos.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }


        private void InitiateFactory(ITypeSymbol symbol, StringBuilder sb)
        {
            var visibility = symbol.Visibility();
            var s =
$@"// /!\ This file is autogeneratiid
namespace {symbol.ContainingNamespace}
{{
    {visibility} partial class {symbol.Name}
    {{";
            sb.AppendLine(s);
        }
        private void FinishFactory(ITypeSymbol _, StringBuilder sb)
        {
            sb.AppendLine($"    }}{Environment.NewLine}}}");
        }

        private void GenerateLogger(IMethodSymbol method, StringBuilder sb)
        {
            var logIface = method.ReturnType;
            var logifacename = logIface.Name;
            var methodname = method.Name;
            var logclassname = $"Generated_{logifacename}";
            var visibility = method.Visibility();
            var logifacevis = logIface.Visibility();
            var s =
$@"        {visibility} partial {logifacename} {methodname}()
            => new {logclassname}();

        {logifacevis} class {logclassname} : InterfaceLogger.AotLogger, {logifacename}
        {{";
            sb.AppendLine(s);

            foreach(var ifmethod in logIface.GetMembers().Where(m => m is IMethodSymbol).Cast<IMethodSymbol>())
            {
                var ifmname = ifmethod.Name;
                var paramdef = ifmethod.Parameters.Select(p => $"{p.OriginalDefinition} {p.Name}").JoinString(", ");
                var paramlst = new[] { ifmname.Quoted() }.Concat(ifmethod.Parameters.Select(p => p.Name)).JoinString(", ");
                s =
$@"            public void {ifmname}({paramdef})
                => base.Log({paramlst});";
                sb.AppendLine(s);
            }
            s = "        }";
            sb.AppendLine(s);
        }

        private ITypeSymbol GetTypeSymbols(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            var decl = (ClassDeclarationSyntax)context.Node;

            if (context.SemanticModel.GetDeclaredSymbol(decl, cancellationToken) is ITypeSymbol typeSymbol)
            {
                return typeSymbol;
            }

            return null;
        }
    }
}
