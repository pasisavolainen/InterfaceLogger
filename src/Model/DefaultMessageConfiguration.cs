using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLogger.Model;

internal class DefaultMessageConfiguration : IMessageConfiguration
{
    public string Text { get; internal set; }

    public LogLevel Level { get; internal set; }
}
