using InterfaceLogger;
using InterfaceLoggerDemo.Properties;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerManager.Get<Program, IDemoLogger>(DemoLogResource.ResourceManager);

            logger.Test();
        }
    }
}
