namespace InterfaceLogger;

public abstract class AotLogger<TLogger>
{
    public TLogger Log(TLogger logger, string msg, params object[] values)
        => LoggerManager.Instance.Log(logger, msg, values);
}
