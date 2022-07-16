using System;
using System.Collections.Generic;
using System.Linq;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLoggerTests.Model
{
    internal class TestComplexSink : ISink
    {
        protected List<IMessageConfiguration> _messages = new List<IMessageConfiguration>();

        public void Write(LogLevel level, string msg, Exception e, params object[] formatParams)
            => _messages.Add(new TestSimpleMessageConfiguration { Text = msg, Level = level });

        internal IMessageConfiguration FirstMessage
            =>_messages.FirstOrDefault();
    }
}
