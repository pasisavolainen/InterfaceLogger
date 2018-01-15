namespace InterfaceLogger.Interfaces
{
    public interface IMessageSource
    {
        IMessageConfiguration GetMessageConfiguration(string name);
    }
}
