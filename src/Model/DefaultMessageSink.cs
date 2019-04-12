using System;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLogger.Model
{
    public class DefaultMessageSink : ISink
    {
        private static ISink _instance;
        public static ISink Instance => _instance ?? (_instance = new DefaultMessageSink());
        public void Write(LogLevel level, string msg, Exception e, params object[] formatParams)
        {
            var logger = LogProvider.For<ISink>();

            logger.Log(level, () => msg, null);
        }
    }
}
