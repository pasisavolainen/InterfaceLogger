using System;
using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Data
{
    public interface IExampleLogger
    {
        void Example();
        void Example(string message);
        void Example(DateTime dt);
    }
    internal class SingleDemoLoggerFactory : ILoggerFactory
    {
        public IExampleLogger ExampleLogger { get; set; }
    }
}
