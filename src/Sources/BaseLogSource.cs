using InterfaceLogger.Logging;

namespace InterfaceLogger.Sources
{
    public abstract class BaseLogSource
    {
        public static LogLevel ResolveLevel(string fmt)
        {
            fmt = fmt?.ToLowerInvariant();
            return fmt switch
            {
                "t" or "trace" or "v" or "verbose" => LogLevel.Trace,
                "d" or "debug" => LogLevel.Debug,
                "i" or "info" => LogLevel.Info,
                "w" or "warn" or "warning" => LogLevel.Warn,
                "e" or "error" => LogLevel.Error,
                "f" or "fatal" => LogLevel.Fatal,
                _ => LogLevel.Info,
            };
        }
    }
}
