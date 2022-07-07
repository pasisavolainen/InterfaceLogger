using Serilog;

namespace Demo
{
    internal class SerilogHelper
    {
        static public void SpinUpSerilog()
        {
            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}{NewLine}{Exception}")
                .CreateLogger();
            Log.Logger = log;
        }
    }
}
