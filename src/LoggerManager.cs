using FakeItEasy;
using FakeItEasy.Core;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Model;

namespace InterfaceLogger
{
    public class LoggerManager
    {
        public static TLog Get<TLog>(ISink sink = null, IMessageSource messageSource = null)
            where TLog : class
        {
            var x = A.Fake<TLog>();
            A.CallTo(x)
                .Invokes(call => DoLogging(call, sink, messageSource ?? GetMessageSource<TLog>()));

            return x;
        }

        private static IMessageSource GetMessageSource<TLog>() where TLog : class
        {
            return DefaultMessageSource.Instance;
        }

        private static void DoLogging(IFakeObjectCall call, ISink sink, IMessageSource messageSource)
        {
            var msgCfg = messageSource.GetMessageConfiguration(call.Method.Name);
            sink.Write(msgCfg.Text);
        }
    }
}
