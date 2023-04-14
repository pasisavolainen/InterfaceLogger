namespace InterfaceLoggerTests;

public interface IBasicsLog
{
    void Message();
    void ParametrizedMessage(int value);
    void MessageWithPriority();
}

public class Basics
{
    [Fact]
    public void Creates()
    {
        var mahLogger = LoggerManager.Get<IBasicsLog>();

        Assert.NotNull(mahLogger);
    }

    [Fact]
    public void Logs()
    {
        var sink = new TestSimpleSink();
        var mahLogger = LoggerManager.Get<IBasicsLog>(sink);

        mahLogger.Message();

        Assert.True(sink.HasMessage(nameof(IBasicsLog.Message)));
    }

    [Fact]
    public void ConfigurableText()
    {
        var sink = new TestSimpleSink();
        var expectedMessage = "The real message";
        var messageSource = new TestSimpleMessageSource()
            .MessageText(nameof(IBasicsLog.Message), expectedMessage);
        var mahLogger = LoggerManager.Get<IBasicsLog>(sink, messageSource);

        mahLogger.Message();

        Assert.True(sink.HasMessage(expectedMessage));
    }

    [Fact]
    public void Parametrization()
    {
        var sink = new TestSimpleSink();
        var msg = "Message {0}";
        var messageSource = new TestSimpleMessageSource()
            .MessageText(nameof(IBasicsLog.ParametrizedMessage), msg);

        var testLogger = LoggerManager.Get<IBasicsLog>(sink, messageSource);

        testLogger.ParametrizedMessage(123);

        Assert.Equal("Message 123", sink.FirstMessage());
    }

    [Fact]
    public void ParametrizationBooBoo()
    {
        var sink = new TestSimpleSink();
        var msg = "Message {999}";
        var messageSource = new TestSimpleMessageSource()
            .MessageText(nameof(IBasicsLog.ParametrizedMessage), msg);

        var testLogger = LoggerManager.Get<IBasicsLog>(sink, messageSource);

        testLogger.ParametrizedMessage(123);

        Assert.Equal(msg, sink.FirstMessage());
    }

    [Fact]
    public void Priority()
    {
        var sink = new TestComplexSink();
        var expectedMessage = "priority test message";
        var expectedLevel = LogLevel.Fatal;
        var messageSource = new TestSimpleMessageSource()
            .MessageText(nameof(IBasicsLog.ParametrizedMessage), expectedMessage, expectedLevel);

        var testLogger = LoggerManager.Get<IBasicsLog>(sink, messageSource);

        testLogger.ParametrizedMessage(123);

        Assert.Equal(expectedMessage, sink.FirstMessage.Text);
        Assert.Equal(expectedLevel, sink.FirstMessage.Level);
    }
}
