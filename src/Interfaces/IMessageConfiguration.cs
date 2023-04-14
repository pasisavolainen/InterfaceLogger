using InterfaceLogger.Logging;

namespace InterfaceLogger.Interfaces;

public interface IMessageConfiguration
{
    string Text { get; }
    LogLevel Level { get; }
}
