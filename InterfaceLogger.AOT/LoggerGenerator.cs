using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace InterfaceLogger.AOT
{
    [Generator]
    public class LoggerGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            //if (!Debugger.IsAttached)
            //    Debugger.Launch();
#endif
            context.RegisterForSyntaxNotifications(() => new LoggerClassReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not LoggerClassReceiver sr)
                return;

            var uc = sr.ClassesToAugment;
            if (uc?.Count == 0)
                return;

            var loggerFactoryInterface = context.Compilation.GetTypeByMetadataName("InterfaceLogger.Interfaces.ILoggerFactory");

            string nmName = uc.First().ContainingNamespace.ToDisplayString();
            StringBuilder sb = new($@"
namespace {nmName}
{{
");

            foreach(var receivedClass in sr.ClassesToAugment)
            {
                sb.Append($"    public partial class {receivedClass.Name} {{ }}");
            }
            sb.AppendLine("\n}\n");

            var st = SourceText.From(sb.ToString(), Encoding.UTF8);
            context.AddSource("hello.Generated", st);
        }

        class LoggerClassReceiver : ISyntaxContextReceiver
        {
            public List<INamedTypeSymbol> ClassesToAugment { get; } = new ();
            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is ClassDeclarationSyntax cds
                    // && cds.Identifier.ValueText == "EmptyDemoLoggerFactory"
                    && cds.BaseList != null)
                {
                    var clSymbol = context.SemanticModel.GetDeclaredSymbol(cds) as INamedTypeSymbol;
                    if (clSymbol.AllInterfaces.Any(ii => ii.ToDisplayString() == "InterfaceLogger.Interfaces.ILoggerFactory"))
                        ClassesToAugment.Add( clSymbol);
                }
            }
        }
    }
}
