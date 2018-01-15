using System.Collections.Generic;
using InterfaceLogger.Interfaces;

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

        internal TestSimpleMessageSource MessageText(string msg, string realText)
        {
            GetOrNew(msg).Text = realText;
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