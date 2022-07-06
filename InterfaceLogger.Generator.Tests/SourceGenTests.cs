using System.Diagnostics;
using System.Reflection;
using InterfaceLogger.AOT;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace InterfaceLoggerTests
{
    public class SourceGenTests
    {
        [Fact]
        public void Test1()
        {
            var source = @"namespace foo { class c { void m() {} } }";

            var output = GetGeneratedOutput(source);

            Assert.NotNull(output);
            Assert.Equal("// global::foo.c", output.Trim());
        }

        [Fact]
        public void SingleDemo()
        {
            var source = TestFile("SingleDemoLoggerFactory.cs");

            // act
            var output = GetGeneratedOutput(source);

            // assert
            Assert.NotNull(output);
            var expected = TestFile("SingleDemoLoggerFactory.Generated.cs");

            Assert.Equal(expected, output);
        }

        private static string TestFile(string fileName)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                        ?? Environment.CurrentDirectory;
            var fullpath = Path.Combine(path, "Data", fileName);
            return File.ReadAllText(fullpath);
        }

        private string GetGeneratedOutput(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            var references = new List<MetadataReference>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (!assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                {
                    references.Add(MetadataReference.CreateFromFile(assembly.Location));
                }
            }

            var compilation = CSharpCompilation.Create("foo", new SyntaxTree[] { syntaxTree }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // TODO: Uncomment this line if you want to fail tests when the injected program isn't valid _before_ running generators
            // var compileDiagnostics = compilation.GetDiagnostics();
            // Assert.False(compileDiagnostics.Any(d => d.Severity == DiagnosticSeverity.Error), "Failed: " + compileDiagnostics.FirstOrDefault()?.GetMessage());

            IIncrementalGenerator generator = new LoggerGenerator();

            var driver = CSharpGeneratorDriver.Create(generator);
            driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var generateDiagnostics);
            Assert.False(generateDiagnostics.Any(d => d.Severity == DiagnosticSeverity.Error), "Failed: " + generateDiagnostics.FirstOrDefault()?.GetMessage());

            string output = outputCompilation.SyntaxTrees.Last().ToString();

            Debug.WriteLine(output);

            return output;
        }
    }
}
