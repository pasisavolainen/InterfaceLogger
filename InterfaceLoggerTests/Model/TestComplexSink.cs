using System.Collections.Generic;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLoggerTests.Model
{
    class TestComplexSink : ISink
    {
        protected List<IMessageConfiguration> _messages = new List<IMessageConfiguration>();

        public void Write(string msg, LogLevel level)
        {
            _messages.Add(new TestSimpleMessageConfiguration { Text = msg, Level = level });
        }

        internal IMessageConfiguration FirstMessage
            =>_messages[0];
    }
}
