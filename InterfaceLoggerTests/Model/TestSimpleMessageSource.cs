using System;
using System.Collections.Generic;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLoggerTests.Model
{
    internal class TestSimpleMessageSource : IMessageSource
    {
        Dictionary<string, TestSimpleMessageConfiguration> Messages { get; } = new Dictionary<string, TestSimpleMessageConfiguration>();

        public IMessageConfiguration GetMessageConfiguration(string name)
        {
            Messages.TryGetValue(name, out TestSimpleMessageConfiguration val);
            return val;
        }

        internal TestSimpleMessageSource MessageText(string callName, string realText, LogLevel level = LogLevel.Info)
        {
            var msg = GetOrNew(callName);
            msg.Text = realText;
            msg.Level = level;
            return this;
        }

        private TestSimpleMessageConfiguration GetOrNew(string msg)
        {
            TestSimpleMessageConfiguration cfg;
            if (!Messages.TryGetValue(msg, out cfg))
                Messages[msg] = cfg = new TestSimpleMessageConfiguration();
            return cfg;
        }
    }
}