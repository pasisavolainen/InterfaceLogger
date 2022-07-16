using System;
using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Data
{
    public interface IExampleLogger
    {
        void Example();
        //void Example(string message);
        //void Example(DateTime dt);
    }
    public partial class SingleDemoLoggerFactory : ILoggerFactory
    {
        public partial IExampleLogger GetExampleLogger();
    }
}
