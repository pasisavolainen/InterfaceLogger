using InterfaceLogger;
using Xunit;

namespace InterfaceLoggerTests
{
    public interface IMyTestLoggerInterface
    {
        void Message();
    }
    public class Basics
    {
        [Fact]
        public void Creates()
        {
            var mahLogger = LoggerManager.Get<IMyTestLoggerInterface>();

            Assert.NotNull(mahLogger);
        }

        [Fact]
        public void Logs()
        {
            var sink = new SimpleSink();
            var mahLogger = LoggerManager.Get<IMyTestLoggerInterface>(sink);

            mahLogger.Message();

            Assert.NotNull(mahLogger);
            Assert.True(sink.HasMessage("Message"));
        }
    }
}
