//using InterfaceLogger;
//using InterfaceLogger.Interfaces;

namespace InterfaceLoggerTests.Data
{
    public interface IExampleLogger
    {
        void Example();
        //void Example(string message);
        //void Example(DateTime dt);
    }
    public partial class SingleDemoLoggerFactory : InterfaceLogger.Interfaces.ILoggerFactory
    {
        public partial IExampleLogger GetExampleLogger();
    }

    public partial class SingleDemoLoggerFactory
    {
        public partial IExampleLogger GetExampleLogger()
            => new ExampleLogger();

        public class ExampleLogger : InterfaceLogger.AotLogger, IExampleLogger
        {
            public void Example()
                => base.Log("Example");
        }
    }
}
