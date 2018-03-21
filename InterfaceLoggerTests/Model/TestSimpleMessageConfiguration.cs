using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Model
{
    internal class TestSimpleMessageConfiguration : IMessageConfiguration
    {
        public string Text { get; set; }

        public Level Level { get; set; }
    }
}
