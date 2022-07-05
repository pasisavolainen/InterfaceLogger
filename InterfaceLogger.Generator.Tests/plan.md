### Plan

See file like:
```csharp

public interface Namespace.ISomeLogger
{
	ISomeLogger Test();
}

// gets generated:
public class Namespace.GeneratedLoggerFactory : ILoggerFactory
{
	ISomeLogger GetSomeLogger();
    TLogger GetLogger<TLogger>() {
		typeof(TLogger) switch {
			typeof(ISomeLogger) => GetSomeLogger(),
            _ => throw new NotSupportedException()
        };
    }
    TLogger Log<TLogger>(string functionName)
    {
        var logger = GetLogger<TLogger>();
        InternalLog(logger, functionName);
        return logger;
    }
}
public class Namespace.GeneratedISomeLogger: ISomeLogger {
    ISomeLogger Test()
        => Namespace.GeneratedLoggerFactory.Log<ISomeLogger>("Test");
}

```
