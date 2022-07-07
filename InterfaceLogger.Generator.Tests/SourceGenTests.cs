namespace InterfaceLoggerTests
{
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
            InlineData("SingleDemoLoggerFactory.cs"),
            InlineData("DemoBroken.cs"),
            InlineData("EmptyDemoLoggerFactory.cs"),
            InlineData("Parameters.cs"),
            InlineData("Return.cs"),
        ]
        public Task Compilations(string fileName)
        {
            var source = FileFromData(fileName);

            // act
            var output = GetGeneratedOutput(source);

            // assert
            return Verify(output)
                    .UseParameters(fileName)
                    .UseDirectory("VerifyData")
                    .UseExtension("g.cs");
        }
    }
}
