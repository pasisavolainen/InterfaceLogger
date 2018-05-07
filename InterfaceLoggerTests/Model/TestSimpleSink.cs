using System.Collections.Generic;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLoggerTests.Model
{
    internal class TestSimpleSink : ISink
    {
        public List<string> Messages { get; } = new List<string>();

        public void Write(string msg, LogLevel level)
            => Messages.Add(msg);

        internal bool HasMessage(string msg)
            => Messages.Contains(msg);

        internal string FirstMessage()
        {
            return Messages[0];
        }
    }
}