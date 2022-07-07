// /!\ This file is autogeneratiid
namespace InterfaceLogger.Generator.Tests.Data.Return
{
    public partial class ReturnLoggerFactory
    {
        public partial IReturnLogger GetExampleLogger()
            => new Generated_IReturnLogger();

        public class Generated_IReturnLogger : InterfaceLogger.AotLogger<IReturnLogger>, IReturnLogger
        {
            public IReturnLogger Example(string message, DateTime? dt)
                => base.Log("Example", message, dt);
        }
    }
}
