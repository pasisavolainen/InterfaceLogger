using InterfaceLogger;
using InterfaceLoggerTests.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InterfaceLoggerTests
{
    public class InjectedShim
    {
        public ISemanticLogger<IBasicsLog> Log { get; }
        public InjectedShim(ISemanticLogger<IBasicsLog> log)
        {
            Log = log;
        }
    }
    public class InjectedBase
    {
        public IBasicsLog Log { get; }
        public InjectedBase(IBasicsLog log)
        {
            Log = log;
        }
    }

    public class DependencyInjection
    {
        [Fact(DisplayName = "Inject logger using generic shim")]
        public void UsingShim()
        {
            // arrange
            var srvCollection = new ServiceCollection();
            srvCollection.AddScoped(typeof(ISemanticLogger<>), typeof(LogShim<>));
            srvCollection.AddScoped<InjectedShim>();

            // act
            var srvProvider = srvCollection.BuildServiceProvider();

            // assert
            var injected = srvProvider.GetRequiredService<InjectedShim>();

            Assert.NotNull(injected.Log);
            Assert.NotNull(injected.Log.Semantic);
            injected.Log.Semantic.Message();
        }

        [Fact(DisplayName = "Inject logger using factory")]
        public void UsingFactory()
        {
            // arrange
            var testSink = new TestSimpleSink();
            var srvCollection = new ServiceCollection();
            srvCollection.AddScoped<IBasicsLog>(sp => LogFactory.BuildLogger<IBasicsLog>(testSink));
            srvCollection.AddScoped<InjectedBase>();

            var srvProvider = srvCollection.BuildServiceProvider();

            // act
            var injected = srvProvider.GetRequiredService<InjectedBase>();
            Assert.NotNull(injected);

            injected.Log.Message();

            // assert
            Assert.Single(testSink.Messages);
        }
    }
}
