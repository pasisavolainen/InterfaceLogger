using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLogger;
using InterfaceLogger.Logging;
using InterfaceLoggerTests.Model;
using Xunit;

namespace InterfaceLoggerTests
{
    internal interface IMini {
        void Foo();
    }
    internal class MinimalAotLogger : AotLogger<IMini>, IMini
    {
        public void Foo() => _ = 0;
    }
    public class AotUsage
    {
        [Fact]
        public void AotLogMessage()
        {
            var options = new object[] { 1, "a", 'c', this };
            var msg = "Message";
            var mini = new MinimalAotLogger();

            // act
            IMini log = LoggerManager.Instance.Log<IMini>(mini, msg, options);

            // assert
            Assert.NotNull(log);
        }

        [Fact]
        public void AotLogMessageSinks()
        {
            var sink = new TestComplexSink();
            var msg = "Message";
            var source = new TestSimpleMessageSource()
                .MessageText(msg, msg, LogLevel.Error);
            var lman = LoggerManager.Instantiate(sink, source);
            var mini = new MinimalAotLogger();
            var options = new object[] { 1, "a", 'c', this };

            // act
            lman.Log(nameof(IMini.Foo), msg, options);

            // ass
            Assert.Equal(msg, sink.FirstMessage.Text);
            //Assert.Equal()
        }
        [Fact]
        public void AotLogMessageFormatted()
        {
            var sink = new TestComplexSink();
            var msg = "Message {0}, {1}, {2}, {3}";
            var expectedLevel = LogLevel.Error;
            var source = new TestSimpleMessageSource()
                .MessageText(msg, msg, expectedLevel);
            var lman = LoggerManager.Instantiate(sink, source);
            var mini = new MinimalAotLogger();
            var options = new object[] { 1, "a", 'c', this };

            // act
            lman.Log(nameof(IMini.Foo), msg, options);

            // ass
            var expectedMessage = "Message 1, a, c, InterfaceLoggerTests.AotUsage";
            Assert.Equal(expectedMessage, sink.FirstMessage.Text);
            Assert.Equal(expectedLevel, sink.FirstMessage.Level);
        }
    }
}
