﻿// /!\ This file is autogeneratiid
namespace InterfaceLoggerTests.Data
{
    public partial class ParametersLoggerFactory
    {
        public partial IParametersLogger GetExampleLogger()
            => new Generated_IParametersLogger();

        public class Generated_IParametersLogger : InterfaceLogger.AotLogger, IParametersLogger
        {
            public void Example(string message, DateTime? dt)
                => base.Log("Example", message, dt);
        }
    }
}
