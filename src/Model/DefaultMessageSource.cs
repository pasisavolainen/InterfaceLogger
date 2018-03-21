using InterfaceLogger.Interfaces;

namespace InterfaceLogger.Model
{
    internal class DefaultMessageSource : IMessageSource
    {
        private static IMessageSource _instance;

        public static IMessageSource Instance => _instance ?? (_instance = new DefaultMessageSource());

        public IMessageConfiguration GetMessageConfiguration(string name)
        {
            return new DefaultMessageConfiguration { Text = name };
        }
    }
}