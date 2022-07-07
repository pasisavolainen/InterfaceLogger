// /!\ This file is autogeneratiid
namespace InterfaceLoggerTests.Data
{
    public partial class SingleDemoLoggerFactory
    {
        public partial IExampleLogger GetExampleLogger()
            => new Generated_IExampleLogger();

        public class Generated_IExampleLogger : InterfaceLogger.AotLogger, IExampleLogger
        {
            public void Example()
                => base.Log("Example");
        }
    }
}
