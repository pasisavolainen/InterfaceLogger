namespace InterfaceLoggerTests.TestModel;

internal class TestSimpleSink : ISink
{
    public List<string> Messages { get; } = new ();

    public void Write(LogLevel level, string msg, Exception e, params object[] formatParams)
        => Messages.Add(msg);

    internal bool HasMessage(string msg)
        => Messages.Contains(msg);

    internal string FirstMessage()
        => Messages.FirstOrDefault();
}