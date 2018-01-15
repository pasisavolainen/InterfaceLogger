using System;
using FakeItEasy;
using FakeItEasy.Core;

namespace InterfaceLogger
{
    public interface ISink
    {
        void Write(string msg);
    }
    public class LoggerManager
    {
        public static TInterface Get<TInterface>(ISink sink = null)
            where TInterface : class
        {
            var x = A.Fake<TInterface>();
            A.CallTo(x)
                //.Where(call => call.Method.Name == "Message")
                .Invokes(call => DoLogging(sink, call));

            return x;
        }

        private static void DoLogging(ISink sink, IFakeObjectCall call)
        {
            sink.Write(call.Method.Name);
        }
    }
}
