using System.Runtime.CompilerServices;

namespace Ewan.Finance.InfraStructure.Interfaces.Logging
{
    public interface ILoggerHelper<TLogger>
    {
        TLogger Logger { get; }

        string GetContextInfoForLogging([CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMethodName = null,
            [CallerLineNumber] int callerLineNumber = 0);
    }
}
