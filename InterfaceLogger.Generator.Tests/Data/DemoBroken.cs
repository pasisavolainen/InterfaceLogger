using InterfaceLogger.Interfaces;

namespace Demo
{
    internal interface IDemoLogger
    {
        void Test();
        void TestNotInResource();
    }
    public partial class MyLogger { }

    internal partial class DemoLoggirFactory : ILoggerFactory
    {
        public partial IDemoLogger DemoLogger();
    }
}