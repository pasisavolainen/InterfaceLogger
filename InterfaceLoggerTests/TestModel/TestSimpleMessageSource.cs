namespace InterfaceLoggerTests.TestModel;

internal class TestSimpleMessageSource : IMessageSource
{
    private Dictionary<string, TestSimpleMessageConfiguration> Messages { get; } = new();

    public IMessageConfiguration GetMessageConfiguration(string name)
    {
        Messages.TryGetValue(name, out var val);
        return val;
    }

    internal TestSimpleMessageSource MessageText(string callName, string realText, LogLevel level = LogLevel.Info)
    {
        var msg = GetOrNew(callName);
        msg.Text = realText;
        msg.Level = level;
        return this;
    }

    private TestSimpleMessageConfiguration GetOrNew(string msg)
    {
        if (!Messages.TryGetValue(msg, out var cfg))
            Messages[msg] = cfg = new();
        return cfg;
    }
}