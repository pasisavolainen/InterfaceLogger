using System.Collections.Generic;
using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Model
{
    internal class TestSimpleSink : ISink
    {
        public List<string> Messages { get; } = new List<string>();

        public void Write(string msg)
            => Messages.Add(msg);

        internal bool HasMessage(string msg)
            => Messages.Contains(msg);
    }
}