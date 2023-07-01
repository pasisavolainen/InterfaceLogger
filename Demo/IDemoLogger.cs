using InterfaceLogger.Interfaces;

namespace InterfaceLoggerDemo;

public interface IDemoLogger
{
    void Test();
    void TestNotInResource();
    void TestWithParameter(string paramname);
    void TestWithMultipleParameters(string paramname, DateTime another);
    IDemoLogger TestWithDemoReturn();
}

public partial class DemoLoggirFactory : ILoggerFactory
{
    public partial IDemoLogger DemoLogger();
}