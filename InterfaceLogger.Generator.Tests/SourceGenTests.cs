using System.Reflection;
using System.Text;
using InterfaceLogger.AOT;
using InterfaceLogger.Interfaces;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using VerifyCS = CSharpSourceGeneratorVerifier<InterfaceLogger.AOT.LoggerGenerator>;

namespace InterfaceLoggerTests
{
    public class SourceGenTests
    {
        [Fact]
        public async Task EmptyDemoLogger()
        {
            await GenerateSingleFileTest("EmptyDemoLoggerFactory").RunAsync();
        }

        [Fact]
        public async Task SingleDemoLogger()
        {
            await GenerateSingleFileTest("SingleDemoLoggerFactory").RunAsync();
        }

        private static VerifyCS.Test GenerateSingleFileTest(string fileName)
        {
            var code = TestFile($"{fileName}.cs");
            var generated = TestFile($"{fileName}.Generated.cs");
            return new VerifyCS.Test
            {
                TestState =
                {
                    Sources = {code},
                    // defaults to netcore3.1 which is not installed..
                    ReferenceAssemblies = ReferenceAssemblies.NetStandard.NetStandard20,
                    AdditionalReferences = { typeof(ILoggerFactory).Assembly },
                    GeneratedSources =
                    {
                        (typeof(LoggerGenerator), "hello.Generated.cs",
                            SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha1))
                    },
                },
            };
        }

        private static string TestFile(string fileName)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                        ?? Environment.CurrentDirectory;
            var fullpath = Path.Combine(path, "Data", fileName);
            return File.ReadAllText(fullpath);
        }
    }
}
