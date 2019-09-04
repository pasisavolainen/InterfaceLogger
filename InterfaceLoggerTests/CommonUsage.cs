using InterfaceLogger;
using InterfaceLoggerTests.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InterfaceLoggerTests
{
    public interface IWantedLog
    {
        void SomeMessage(int x);
    }
    // Contextless use, so 'DirectUser' is unknown to IWantedLog implementation and will
    // not show up anywhere in the log.
    public class DirectUser
    {
        private IWantedLog Log { get; }
        public DirectUser(IWantedLog log)
        {
            Log = log;
        }
        public void TriggerLog(int x) => Log.SomeMessage(x);
    }
    public class FactoryUser
    {
        private IWantedLog Log { get; }
        public FactoryUser(LoggerProvider<IWantedLog> log)
        {
            Log = log.For(this);
        }
        public void TriggerLog(int x) => Log.SomeMessage(x);
    }
    public class CommonUsage
    {
        [Fact(DisplayName = "Injected as iface directly, so doesn't have context")]
        public void DirectUse()
        {
            // arrange
            var testSink = new TestSimpleSink();
            var srvCollection = new ServiceCollection();
            srvCollection.AddTransient<IWantedLog>(sp => LogFactory.BuildLogger<IWantedLog>(testSink));
            srvCollection.AddScoped<DirectUser>();

            var srvProvider = srvCollection.BuildServiceProvider();

            // act
            var directUser = srvProvider.GetRequiredService<DirectUser>();
            Assert.NotNull(directUser);

            directUser.TriggerLog(42);

            // assert
            Assert.Single(testSink.Messages);
        }

        [Fact(DisplayName = "Injected as factory, has context")]
        public void FactoryUse()
        {
            // arrange
            var testSink = new TestSimpleSink();
            var srvCollection = new ServiceCollection();
            //srvCollection.AddTransient<IWantedLog>(sp => LogFactory.BuildLogger<IWantedLog>(testSink));
            srvCollection.AddScoped(typeof(LoggerProvider<>), typeof(LoggerProvider<>));
            srvCollection.AddScoped<FactoryUser>();
            LoggerManager.RegisterContextSink<FactoryUser>(testSink);

            var srvProvider = srvCollection.BuildServiceProvider();

            // act
            var factoryUser = srvProvider.GetRequiredService<FactoryUser>();
            Assert.NotNull(factoryUser);

            factoryUser.TriggerLog(42);

            // assert
            Assert.Single(testSink.Messages);
        }
    }
}
