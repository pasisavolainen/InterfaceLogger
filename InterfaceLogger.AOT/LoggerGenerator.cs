using System.Diagnostics;
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
            LoggerClassReceiver sr = (LoggerClassReceiver)context.SyntaxReceiver;
            var uc = sr.ClassToAugment;
            if (uc == null)
                return;

            var st = SourceText.From(@"
namespace GenDemo {
    public partial class hello {
    }
}
",
                Encoding.UTF8);
            context.AddSource("hello.Generated", st);
        }

        class LoggerClassReceiver : ISyntaxReceiver
        {
            public ClassDeclarationSyntax ClassToAugment { get; private set; }
            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax cds
                    && cds.Identifier.ValueText == "DemoLoggerFactory")
                {
                    ClassToAugment = cds;
                }
            }
        }
    }
}
