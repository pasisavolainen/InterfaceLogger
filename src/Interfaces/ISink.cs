namespace InterfaceLogger.Interfaces
{
    public interface ISink
    {
        void Write(string msg, Level level);
    }
}
