using InterfaceLogger.Sources;

namespace InterfaceLoggerTests;

public class SourceParameters
{
    [Theory,
        InlineData(LogLevel.Trace, new[] { "t", "trace", "v", "verbose", "VERBOSE" }),
        InlineData(LogLevel.Debug, new[] { "d", "debug" }),
        InlineData(LogLevel.Info, new[] { "i", "info" }),
        InlineData(LogLevel.Warn, new[] { "w", "warn", "warning" }),
        InlineData(LogLevel.Error, new[] { "e", "error", "e" }),
        InlineData(LogLevel.Fatal, new[] { "f", "fatal" }),
        InlineData(LogLevel.Info, new[] { "nope", "fa", "äöä", "///"})
        ]
    public void LevelParses(LogLevel expectedLevel, string[] strings)
    {
        foreach (var s in strings)
        {
            var resultLevel = BaseLogSource.ResolveLevel(s);

            Assert.Equal(expectedLevel, resultLevel);
        }
    }
}
