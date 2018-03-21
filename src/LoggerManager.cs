using System;
using System.Linq;
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
            var formattedMsg = msgCfg.Text;
            var level = msgCfg.Level;
            try
            {
                formattedMsg = string.Format(msgCfg.Text, call.Arguments.ToArray());
            } catch(Exception )
            {
                // do the needful
            }
            //IFormattable abs = $"123 {0}, {call.Arguments[0]}";
            sink.Write(formattedMsg, level);
        }
    }
}
