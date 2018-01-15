using System;

namespace InterfaceLogger
{
    public class LoggerManager
    {
        public static TInterface Get<TInterface>()
        {
            return default(TInterface);
        }
    }
}
