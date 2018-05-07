using InterfaceLogger;
using InterfaceLoggerDemo.Properties;
using Serilog;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SpinUpSerilog();
            var logger = LoggerManager.Get<Program, IDemoLogger>(DemoLogResource.ResourceManager);

            logger.Test();
        }

        private static void SpinUpSerilog()
        {
            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}{NewLine}{Exception}")
                .CreateLogger();
            Log.Logger = log;
        }
    }
}
