using System;
using System.Collections.Generic;
using System.Linq;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLoggerTests.Model
{
    internal class TestSimpleSink : ISink
    {
        public List<string> Messages { get; } = new List<string>();

        public void Write(LogLevel level, string msg, Exception e, params object[] formatParams)
            => Messages.Add(msg);

        internal bool HasMessage(string msg)
            => Messages.Contains(msg);

        internal string FirstMessage()
            => Messages.FirstOrDefault();
    }
}