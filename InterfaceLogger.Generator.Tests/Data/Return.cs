using InterfaceLogger.Interfaces;

namespace InterfaceLogger.Generator.Tests.Data.Return
{
    public interface IReturnLogger
    {
        IReturnLogger Example(string message, DateTime? dt);
    }
    public partial class ReturnLoggerFactory : ILoggerFactory
    {
        public partial IReturnLogger GetExampleLogger();
    }
}
