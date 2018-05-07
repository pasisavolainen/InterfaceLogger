using InterfaceLogger.Logging;

namespace InterfaceLogger.Interfaces
{
    public interface ISink
    {
        void Write(string msg, LogLevel level);
    }
}
