using Microsoft.Extensions.Configuration;
using Serilog;

namespace Ewan.Finance.InfraStructure.Interfaces.Logging
{
    public interface ISerilogHelper : ILoggerHelper<ILogger>
    {
        ILogger InitializeLogger(IConfigurationRoot configuration, string configurationSection);
    }
}
