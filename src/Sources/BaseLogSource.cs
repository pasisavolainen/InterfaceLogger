using InterfaceLogger.Interfaces;

namespace InterfaceLogger.Sources
{
    public abstract class BaseLogSource
    {
        public static Level ResolveLevel(string fmt)
        {
            fmt = fmt?.ToLowerInvariant();
            switch (fmt)
            {
                case "t":
                case "trace":
                case "v":
                case "verbose": return Level.Verbose;
                case "d":
                case "debug": return Level.Debug;
                case "i":
                case "info": return Level.Info;
                case "w":
                case "warn": return Level.Warn;
                case "e":
                case "error": return Level.Error;
                case "f":
                case "fatal": return Level.Fatal;
                default: return Level.Info;
            }
        }
    }
}
