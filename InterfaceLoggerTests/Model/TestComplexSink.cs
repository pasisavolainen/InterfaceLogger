using System.Collections.Generic;
using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Model
{
    class TestComplexSink : ISink
    {
        protected List<IMessageConfiguration> _messages = new List<IMessageConfiguration>();

        public void Write(string msg, Level level)
        {
            _messages.Add(new TestSimpleMessageConfiguration { Text = msg, Level = level });
        }

        internal IMessageConfiguration FirstMessage
            =>_messages[0];        
    }
}
