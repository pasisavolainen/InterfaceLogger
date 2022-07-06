using System.Runtime.CompilerServices;
using InterfaceLogger.Interfaces;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Demo
{
    public interface IDemoLogger
    {
        void Test();
        void TestNotInResource();
    }
    public partial class MyLogger { }

    public partial class DemoLoggirFactory : ILoggerFactory
    {
        public partial IDemoLogger DemoLogger();
    }
}