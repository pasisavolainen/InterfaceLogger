namespace InterfaceLoggerTests.TestModel;

internal class TestComplexSink : ISink
{
    protected List<IMessageConfiguration> _messages = new List<IMessageConfiguration>();

    public void Write(LogLevel level, string msg, Exception e, params object[] formatParams)
        => _messages.Add(new TestSimpleMessageConfiguration { Text = msg, Level = level });

    internal IMessageConfiguration FirstMessage
        =>_messages.FirstOrDefault();
}
