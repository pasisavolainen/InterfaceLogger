﻿using InterfaceLogger;
using InterfaceLoggerTests.Model;
using Xunit;

namespace InterfaceLoggerTests
{
    public interface IBasicsLog
    {
        void Message();
    }
    public class Basics
    {
        [Fact]
        public void Creates()
        {
            var mahLogger = LoggerManager.Get<IBasicsLog>();

            Assert.NotNull(mahLogger);
        }

        [Fact]
        public void Logs()
        {
            var sink = new TestSimpleSink();
            var mahLogger = LoggerManager.Get<IBasicsLog>(sink);

            mahLogger.Message();

            Assert.True(sink.HasMessage(nameof(IBasicsLog.Message)));
        }

        [Fact]
        public void ConfigurableText()
        {
            var sink = new TestSimpleSink();
            var realmessage = "The real messsage";
            var messageSource = new TestSimpleMessageSource()
                .MessageText(nameof(IBasicsLog.Message), realmessage);
            var mahLogger = LoggerManager.Get<IBasicsLog>(sink, messageSource);

            mahLogger.Message();

            Assert.True(sink.HasMessage(realmessage));
        }
    }
}