using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLogger.Model
{
    public class DefaultMessageSink : ISink
    {
        private static ISink _instance;
        public static ISink Instance => _instance ?? (_instance = new DefaultMessageSink());
        public void Write(string msg, LogLevel level)
        {
            var logger = LogProvider.For<ISink>();

            logger.Log(level, () => msg, null);
        }
    }
}
