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
        protected static Dictionary<Type, ISink> LoggerSinks = new Dictionary<Type, ISink>();
        protected static Dictionary<Type, ISink> ContextSinks = new Dictionary<Type, ISink>();

        private class LoggerContext
        {
            public Type LogType { get; set; }
            public Type ContextType { get; set; }
            public WeakReference Context { get; set; }
            public ISink Sink { get; set; }
            public IMessageSource MessageSource { get; set; }
        }
        /// <summary>
        /// Get implementation for logger interface you specify.
        /// </summary>
        /// <typeparam name="TLog"></typeparam>
        /// <param name="sink">Custom sink</param>
        /// <param name="messageSource">custom message source</param>
        /// <returns></returns>
        public static TLog Get<TLog>(ISink sink = null, IMessageSource messageSource = null, object loggercontext = null)
            where TLog : class
        {
            var context = new LoggerContext {
                LogType = typeof(TLog),
                ContextType = loggercontext?.GetType(),
                Context = new WeakReference(loggercontext),
                Sink = sink,
                MessageSource = messageSource,
            };
            var x = A.Fake<TLog>();
            A.CallTo(x)
                .Invokes(call => DoLogging(call, context));

            return x;
        }

        public static TLog Get<TLogContext, TLog>(ResourceManager resourceManager, TLogContext obj)
            where TLog : class
            => Get<TLog>(null, new ResXLogSource(resourceManager), loggercontext: obj);

        internal static ISink GetLoggerSink(Type typeOfLogger)
        {
            if (!LoggerSinks.TryGetValue(typeOfLogger, out ISink sink))
            {
                LoggerSinks[typeOfLogger] = sink = DefaultMessageSink.Instance;
            }
            return sink;
        }
        internal static ISink GetContextSink(Type typeOfContext)
        {
            if(!ContextSinks.TryGetValue(typeOfContext, out ISink sink))
            {
                ContextSinks[typeOfContext] = sink = DefaultMessageSink.Instance;
            }
            return sink;
        }
        internal static void RegisterLoggerSink<TLog>(ISink sink)
        {
            if (LoggerSinks.TryGetValue(typeof(TLog), out ISink oldSink)
                && oldSink != DefaultMessageSink.Instance && oldSink != sink)
            {
                Trace.Write($"Competing sink attempted for {typeof(TLog)}, {sink} when {oldSink} already used.");
            }
            LoggerSinks[typeof(TLog)] = sink;
        }

        private static IMessageSource GetMessageSource(Type type)
        {
            return DefaultMessageSource.Instance;
        }

        public static void RegisterContextSink<TContext>(ISink sink)
        {
            ContextSinks[typeof(TContext)] = sink;
        }

        private static void DoLogging(IFakeObjectCall call, LoggerContext context)
        {
            try
            {
                var messageSource = context.MessageSource ?? GetMessageSource(context.LogType);
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
                var sink = context.Sink ?? GetContextSink(context.ContextType) ?? GetLoggerSink(context.LogType);
                sink.Write(level, formattedMsg, null);
            } catch(Exception e)
            {
                // ok, so one of the components failed.
                Trace.Write(e);
            }
        }
    }
}
