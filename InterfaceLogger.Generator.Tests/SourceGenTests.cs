namespace InterfaceLoggerTests;

[UsesVerify]
public class SourceGenTests
{
    [Fact]
    public void GeneratorIsNotTriggered()
    {
        var source = @"namespace foo { class c { void m() {} } }";

        // act
        var output = GetGeneratedOutput(source);

        Verify(output);
    }

    [Theory,
        InlineData("SingleDemoLoggerFactory"),
        InlineData("DemoBroken"),
        InlineData("EmptyDemoLoggerFactory"),
        InlineData("Parameters"),
        InlineData("Return"),
    ]
    public Task Compilations(string fileName)
    {
        var source = FileFromData($"{fileName}.cs");

        // act
        var output = GetGeneratedOutput(source);

        // assert
        return Verify(output, extension: "cs")
                .UseDirectory("Data")
                .UseFileName(fileName)
                ;
    }
}
