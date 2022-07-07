using InterfaceLogger.Interfaces;

namespace Demo
{
    public interface IDemoLogger
    {
        void Test();
        void TestNotInResource();
        void TestWithParameters(string paramname);
        IDemoLogger TestWithDemoReturn();
    }

    public  partial class DemoLoggirFactory : ILoggerFactory
    {
        public partial IDemoLogger DemoLogger();
    }
}