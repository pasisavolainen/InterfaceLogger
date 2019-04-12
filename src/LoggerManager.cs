using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using FakeItEasy;
using FakeItEasy.Core;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Model;
using InterfaceLogger.Sources;

namespace InterfaceLogger
{
    public class LoggerManager
    {
        protected static Dictionary<Type, ISink> _sinks = new Dictionary<Type, ISink>();
        /// <summary>
        /// Get implementation for logger interface you specify.
        /// </summary>
        /// <typeparam name="TLog"></typeparam>
        /// <param name="sink">Custom sink</param>
        /// <param name="messageSource">custom message source</param>
        /// <returns></returns>
        public static TLog Get<TLog>(ISink sink = null, IMessageSource messageSource = null)
            where TLog : class
        {
            var x = A.Fake<TLog>();
            A.CallTo(x)
                .Invokes(call => DoLogging(call, sink ?? GetSink<TLog>(null),
                                            messageSource ?? GetMessageSource<TLog>()));

            return x;
        }

        public static TLog Get<TLogContext, TLog>(ResourceManager resourceManager)
            where TLog : class
            => Get<TLog>(null, new ResXLogSource(resourceManager));


        internal static ISink GetSink<TLog>(TLog _ = null)
        {
            if (!_sinks.TryGetValue(typeof(TLog), out ISink sink))
            {
                _sinks[typeof(TLog)] = sink = DefaultMessageSink.Instance;
            }
            return sink;
        }
        internal static void SetSink<TLog>(ISink sink)
        {
            if (_sinks.TryGetValue(typeof(TLog), out ISink oldSink)
                && oldSink != DefaultMessageSink.Instance && oldSink != sink)
            {
                Trace.Write($"Competing sink attempted for {typeof(TLog)}, {sink} when {oldSink} already used.");
            }
            _sinks[typeof(TLog)] = sink;
        }

        private static IMessageSource GetMessageSource<TLog>() where TLog : class
        {
            return DefaultMessageSource.Instance;
        }

        private static void DoLogging(IFakeObjectCall call, ISink sink, IMessageSource messageSource)
        {
            try
            {
                var msgCfg = messageSource.GetMessageConfiguration(call.Method.Name);
                var formattedMsg = msgCfg.Text;
                var level = msgCfg.Level;
                try
                {
                    formattedMsg = string.Format(msgCfg.Text, call.Arguments.ToArray());
                }
                catch (Exception)
                {
                    // do the needful
                }
                //IFormattable abs = $"123 {0}, {call.Arguments[0]}";
                sink.Write(formattedMsg, level);
            } catch(Exception e)
            {
                // ok, so one of the components failed.
                Trace.Write(e);
            }
        }
    }
}
