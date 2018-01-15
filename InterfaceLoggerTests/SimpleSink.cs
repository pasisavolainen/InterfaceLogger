using System;
using System.Collections.Generic;
using InterfaceLogger;

namespace InterfaceLoggerTests
{
    internal class SimpleSink : ISink
    {
        public List<string> Messages { get; } = new List<string>();

        public void Write(string msg)
            => Messages.Add(msg);

        internal bool HasMessage(string msg)
            => Messages.Contains(msg);
    }
}