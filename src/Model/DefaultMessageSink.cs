using System.Diagnostics;
using InterfaceLogger.Interfaces;

namespace InterfaceLogger.Model
{
    public class DefaultMessageSink : ISink
    {
        private static ISink _instance;
        public static ISink Instance => _instance ?? (_instance = new DefaultMessageSink());
        public void Write(string msg, Level level)
        {
            Debug.WriteLine(msg, level.ToString());
        }
    }
}
