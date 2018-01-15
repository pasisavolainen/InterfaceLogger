using InterfaceLogger.Interfaces;

namespace InterfaceLogger.Model
{
    internal class DefaultMessageConfiguration : IMessageConfiguration
    {
        public string Text { get; internal set; }
    }
}
