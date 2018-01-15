using InterfaceLogger;
using Xunit;

namespace InterfaceLoggerTests
{
    public interface IMyTestLoggerInterface
    {
    }
    public class Basics
    {
        [Fact]
        public void Creates()
        {
            var mahLogger = LoggerManager.Get<IMyTestLoggerInterface>();

            Assert.NotNull(mahLogger);
        }
    }
}
