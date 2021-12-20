using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InterfaceLogger.AOT;
using InterfaceLogger.Interfaces;
using Microsoft.CodeAnalysis.Text;
using Xunit;
using VerifyCS = CSharpSourceGeneratorVerifier<InterfaceLogger.AOT.LoggerGenerator>;

namespace InterfaceLoggerTests
{
    public class SourceGenTests
    {
        [Fact]
        public async Task hello()
        {
            var code      = TestFile("EmptyDemoLoggerFactory.cs");
            var generated = TestFile("EmptyDemoLoggerFactory.Generated.cs");

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = {code},
                    AdditionalReferences = { typeof(ILoggerFactory).Assembly },
                    GeneratedSources =
                    {
                        (typeof(LoggerGenerator), "hello.Generated.cs", 
                            SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    },
                },
            }.RunAsync();
        }

        private static string TestFile(string fileName)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fullpath = Path.Combine(path, "Data", fileName);
            return File.ReadAllText(fullpath);
        }
    }
}
