using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLoggerTests.Model
{
    internal class TestSimpleMessageConfiguration : IMessageConfiguration
    {
        public string Text { get; set; }

        public LogLevel Level { get; set; }
    }
}
