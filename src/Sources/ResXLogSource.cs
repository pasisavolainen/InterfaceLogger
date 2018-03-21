using System;
using System.Resources;
using InterfaceLogger.Interfaces;
using InterfaceLogger.Model;

namespace InterfaceLogger.Sources
{
    public class ResXLogSource : IMessageSource
    {
        protected ResourceManager RM { get; }

        public ResXLogSource(ResourceManager rm)
        {
            RM = rm;
        }

        public static IMessageSource FromManager(ResourceManager rm)
        {
            if (rm == null)
                throw new Exception("come on mon, don't do dis to me");
            return new ResXLogSource(rm);
        }

        public IMessageConfiguration GetMessageConfiguration(string name)
        {
            return new DefaultMessageConfiguration
            {
                Text = RM.GetString(name),
                Level = Level.Info,
            };
        }
    }
}
