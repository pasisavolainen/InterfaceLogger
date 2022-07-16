using System;
using System.Resources;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Model;

namespace InterfaceLogger.Sources
{
    public class ResXLogSource : BaseLogSource, IMessageSource
    {
        protected ResourceManager RM { get; }

        public ResXLogSource(ResourceManager rm)
        {
            RM = rm;
        }

        public static IMessageSource FromManager(ResourceManager rm)
        {
            _ = rm ?? throw new ArgumentNullException(nameof(rm));
            return new ResXLogSource(rm);
        }

        public IMessageConfiguration GetMessageConfiguration(string name)
        {
            return new DefaultMessageConfiguration
            {
                Text = RM.GetString(name),
                Level = ResolveLevel(RM.GetString(name + '_')),
            };
        }
    }
}
