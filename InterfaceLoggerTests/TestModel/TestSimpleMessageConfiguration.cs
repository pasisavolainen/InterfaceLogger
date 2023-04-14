namespace InterfaceLoggerTests.TestModel;

internal class TestSimpleMessageConfiguration : IMessageConfiguration
{
    public string Text { get; set; }

    public LogLevel Level { get; set; }
}
