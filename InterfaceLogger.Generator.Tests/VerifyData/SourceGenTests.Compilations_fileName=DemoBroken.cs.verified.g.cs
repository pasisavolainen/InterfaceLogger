// /!\ This file is autogeneratiid
namespace Demo
{
    internal partial class DemoLoggirFactory
    {
        internal partial IDemoLogger DemoLogger()
            => new Generated_IDemoLogger();

        internal class Generated_IDemoLogger : InterfaceLogger.AotLogger, IDemoLogger
        {
            public void Test()
                => base.Log("Test");
            public void TestNotInResource()
                => base.Log("TestNotInResource");
        }
    }
}
