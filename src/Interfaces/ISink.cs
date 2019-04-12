using System;
using InterfaceLogger.Logging;

namespace InterfaceLogger.Interfaces
{
    public interface ISink
    {
        void Write(LogLevel level, string msg, Exception e, params object[] formatParams);
    }
}
