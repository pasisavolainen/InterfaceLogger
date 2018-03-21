using InterfaceLogger.Interfaces;
using InterfaceLogger.Sources;
using Xunit;

namespace InterfaceLoggerTests
{
    public class SourceParameters
    {
        [Theory,
            InlineData(Level.Verbose, new[] { "t", "trace", "v", "verbose", "VERBOSE" }),
            InlineData(Level.Debug, new[] { "d", "debug" }),
            InlineData(Level.Info, new[] { "i", "info" }),
            InlineData(Level.Warn, new[] { "w", "warn", "warning" }),
            InlineData(Level.Error, new[] { "e", "error", "e" }),
            InlineData(Level.Fatal, new[] { "f", "fatal" }),
            InlineData(Level.Info, new[] { "nope", "fa", "äöä", "///"})
            ]
        public void LevelParses(Level expectedLevel, string[] strings)
        {
            foreach (var s in strings)
            {
                var resultLevel = BaseLogSource.ResolveLevel(s);

                Assert.Equal(expectedLevel, resultLevel);
            }
        }
    }
}
