using System.Runtime.CompilerServices;
using InterfaceLogger.Interfaces;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Demo
{
    internal interface IDemoLogger
    {
        void Test();
        void TestNotInResource();
    }
    public partial class MyLogger { }

    internal partial class DemoLoggerFactory : ILoggerFactory
    {
        IDemoLogger DemoLogger { get; }
    }
}