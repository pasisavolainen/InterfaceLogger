using System;

namespace InterfaceLogger
{
    public abstract class AotLogger<TLogger>
    {
        public TLogger Log(string msg, params object[] values)
            => throw new NotImplementedException();
    }
}
