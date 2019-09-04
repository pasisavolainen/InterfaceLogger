using System;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Logging;

namespace InterfaceLogger
{
    public interface ISaneLogger
    {
        ISaneLogger Write(LogLevel level, string msg, Exception e, params object[] formatParams);
        ISaneLogger Error(string msg, params object[] formatParams);
        ISaneLogger Debug(string msg, params object[] formatParams);

    }
    public interface ISemanticLogger<T>: ISaneLogger
        where T: class
    {
        T Semantic { get; }
    }
    public class LogShim<T>: ISemanticLogger<T>
        where T: class
    {
        protected T _semantic;
        public T Semantic => _semantic
                        ?? (_semantic = LoggerManager.Get<T>());
        public LogShim()
        { }
        public LogShim(T semantic)
        {
            _semantic = semantic;
        }

        protected ISink _sink;
        public ISaneLogger Write(LogLevel level, string msg, Exception e, params object[] formatParams)
        {
            if (_sink == null)
                _sink = LoggerManager.GetLoggerSink(typeof(T));
            _sink.Write(level, msg, e, formatParams);
            return this;
        }

        public ISaneLogger Error(string msg, params object[] formatParams)
            => Write(LogLevel.Error, msg, null, formatParams);

        public ISaneLogger Debug(string msg, params object[] formatParams)
            => Write(LogLevel.Debug, msg, null, formatParams);
    }

    public class LogFactory
    {
        public LogFactory() { }

        public static TLog BuildLogger<TLog>(ISink sink = null, IMessageSource source = null)
            where TLog: class
            => LoggerManager.Get<TLog>(sink, source);

        public ISemanticLogger<TLog> BuildShim<TLog>(ISink sink, IMessageSource source)
            where TLog : class
            => new LogShim<TLog>(BuildLogger<TLog>(sink, source));
    }

    public class LoggerProvider<TLog> where TLog: class
    {
        public TLog For<TContext>(TContext ctx)
            where TContext: class
            => LoggerManager.Get<TLog>(loggercontext:ctx);
    }
}
