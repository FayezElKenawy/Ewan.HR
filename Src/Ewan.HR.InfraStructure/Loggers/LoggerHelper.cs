using Microsoft.AspNetCore.Http;
using Ewan.Finance.InfraStructure.Interfaces.Logging;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ewan.Finance.InfraStructure.Loggers
{
    public abstract class LoggerHelper<TLogger> : ILoggerHelper<TLogger>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggerHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callerFilePath"></param>
        /// <param name="callerMethodName"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public virtual string GetContextInfoForLogging([CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMethodName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            StringBuilder exceptionContextInfo = new();

            exceptionContextInfo.AppendLine();
            exceptionContextInfo.AppendLine("--------------------------------------------------");
            exceptionContextInfo.AppendLine("*** Context Info ***");

            if (_httpContextAccessor != null)
            {
                exceptionContextInfo.AppendLine($"@Request Method: \"{_httpContextAccessor.HttpContext?.Request.Method}\"");
                exceptionContextInfo.AppendLine($"@Request Path: \"{_httpContextAccessor.HttpContext?.Request.Path}\"");

                if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Request.QueryString.Value))
                {
                    exceptionContextInfo.AppendLine($"@Query String: {_httpContextAccessor.HttpContext.Request.QueryString.Value}");
                }
                //if (httpContextAccessor.HttpContext.Request.Body.ToString() != null)
                //{

                //}      
            }
            else
            {
                exceptionContextInfo.AppendLine("httpContextAccessor is NULL, no info to log!");
            }
            
            //StackTrace stackTrace = new StackTrace();
            //exceptionContextInfo.Append($"@Method Name: \"{stackTrace.GetFrame(1).GetMethod().Name}\", ");
            exceptionContextInfo.AppendLine($"@Method Name: \"{callerMethodName}\"");
            exceptionContextInfo.AppendLine($"@File Path: \"{callerFilePath}\"");
            exceptionContextInfo.AppendLine($"@Line Number: \"{callerLineNumber}\"");
            exceptionContextInfo.AppendLine("--------------------------------------------------");
            return exceptionContextInfo.ToString();
        }

        public abstract TLogger Logger { get; }
    }
}
