using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Data
{
    public interface IParametersLogger
    {
        void Example(string message);
        void Example(string message, DateTime? dt);
    }
    public partial class ParametersLoggerFactory : ILoggerFactory
    {
        public partial IParametersLogger GetExampleLogger();
    }
}
