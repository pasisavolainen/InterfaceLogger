using InterfaceLogger.Logging;

namespace InterfaceLogger.Sources
{
    public abstract class BaseLogSource
    {
        public static LogLevel ResolveLevel(string fmt)
        {
            fmt = fmt?.ToLowerInvariant();
            switch (fmt)
            {
                case "t":
                case "trace":
                case "v":
                case "verbose": return LogLevel.Trace;
                case "d":
                case "debug": return LogLevel.Debug;
                case "i":
                case "info": return LogLevel.Info;
                case "w":
                case "warn":
                case "warning": return LogLevel.Warn;
                case "e":
                case "error": return LogLevel.Error;
                case "f":
                case "fatal": return LogLevel.Fatal;
                default: return LogLevel.Info;
            }
        }
    }
}
