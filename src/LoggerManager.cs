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
        private static LoggerManager g_instance;
        public static LoggerManager Instance {
            get => g_instance ??= Instantiate(DefaultMessageSink.Instance, DefaultMessageSource.Instance);
        }
        protected Dictionary<Type, ISink> LoggerSinks = new();
        protected Dictionary<Type, ISink> ContextSinks = new();

        public ISink GlobalSink { get; private set; }
        public IMessageSource GlobalSource { get; private set; }

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
            if (!Instance.LoggerSinks.TryGetValue(typeOfLogger, out ISink sink))
            {
                Instance.LoggerSinks[typeOfLogger] = sink = DefaultMessageSink.Instance;
            }
            return sink;
        }
        internal static ISink GetContextSink(Type typeOfContext)
        {
            if(!Instance.ContextSinks.TryGetValue(typeOfContext, out ISink sink))
            {
                Instance.ContextSinks[typeOfContext] = sink = DefaultMessageSink.Instance;
            }
            return sink;
        }
        internal static void RegisterLoggerSink<TLog>(ISink sink)
        {
            if (Instance.LoggerSinks.TryGetValue(typeof(TLog), out ISink oldSink)
                && oldSink != DefaultMessageSink.Instance && oldSink != sink)
            {
                Trace.Write($"Competing sink attempted for {typeof(TLog)}, {sink} when {oldSink} already used.");
            }
            Instance.LoggerSinks[typeof(TLog)] = sink;
        }

        private IMessageSource GetMessageSource(Type type)
            => GlobalSource ?? DefaultMessageSource.Instance;

        public static void RegisterContextSink<TContext>(ISink sink)
        {
            Instance.ContextSinks[typeof(TContext)] = sink;
        }

        private static void DoLogging(IFakeObjectCall call, LoggerContext context)
        {
            try
            {
                var messageSource = context.MessageSource ?? Instance.GetMessageSource(context.LogType);
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

        public TLogger Log<TLogger>(TLogger logger, string msg, params object[] options)
        {
            var sink = GlobalSink;
            var messageSource = GetMessageSource(typeof(TLogger));
            var msgInfo = messageSource.GetMessageConfiguration(msg);
            var formattedMsg = msgInfo.Text;
            try
            {
                formattedMsg = string.Format(msgInfo.Text, options);
            }
            catch (Exception e)
            {
                formattedMsg += " -- Format error: " + e.Message;
            }

            sink.Write(msgInfo.Level, formattedMsg, null, options);

            return logger;
        }

        public static LoggerManager Instantiate(ISink sink, IMessageSource source)
            => new() {
                GlobalSink = sink,
                GlobalSource = source,
            };
    }
}
