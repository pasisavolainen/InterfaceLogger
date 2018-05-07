using InterfaceLogger;
using InterfaceLogger.Logging;
using InterfaceLogger.Sources;
using InterfaceLoggerTests.Model;
using InterfaceLoggerTests.Properties;
using Xunit;

namespace InterfaceLoggerTests
{
    public class ResXSource
    {
        [Fact]
        public void ResxWorks()
        {
            var sink = new TestSimpleSink();
            var logger = LoggerManager.Get<IBasicsLog>
                    (sink, ResXLogSource.FromManager(Resources.ResourceManager));

            logger.Message();

            Assert.Equal(Resources.Message, sink.FirstMessage());
        }

        [Fact]
        public void ResXUsesLevel()
        {
            var sink = new TestComplexSink();
            var logger = LoggerManager.Get<IBasicsLog>
                    (sink, ResXLogSource.FromManager(Resources.ResourceManager));

            logger.MessageWithPriority();

            Assert.Equal(Resources.MessageWithPriority, sink.FirstMessage.Text);
            Assert.Equal(LogLevel.Fatal, sink.FirstMessage.Level);
        }
    }
}
