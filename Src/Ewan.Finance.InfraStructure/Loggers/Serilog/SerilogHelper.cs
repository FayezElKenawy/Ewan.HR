using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Ewan.Finance.InfraStructure.Interfaces.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace Ewan.Finance.InfraStructure.Loggers.Serilog
{
    public class SerilogHelper : LoggerHelper<ILogger>, ISerilogHelper
    {
        ILogger _logger;
        static ILogger _staticLogger;

        #region Constructors
        /// <summary>
        /// When using this constructor the logger is initialized with default configurtion and default configuration section, 
        /// call InitializeLogger method to initialize the logger with new configuration
        /// *** This constructor is intended for Dependency Injection ***
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public SerilogHelper(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            if (_logger == null)
            {
                _logger = CreateLogger(BuildConfiguration());
            }
        }

        /// <summary>
        /// When using this constructor the logger is initialized with default configurtion and the provided configuration section, 
        /// call InitializeLogger method to initialize the logger with new configuration
        /// /// *** This Constructor is not used and cannot be used by the Dependency Injection Container ***
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public SerilogHelper(IHttpContextAccessor httpContextAccessor, string configurationSection)
            : base(httpContextAccessor)
        {
            if (_logger == null)
            {
                _logger = CreateLogger(BuildConfiguration(), configurationSection);
            }
        }

        /// <summary>
        /// This constructor Initializes the logger using the provided configuration, call InitializeLogger method
        /// to initialize the logger with new configuration
        /// *** This Constructor is not used and cannot be used by the Dependency Injection Container ***
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpContextAccessor"></param>
        public SerilogHelper(IConfigurationRoot configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _logger = CreateLogger(configuration);
        }

        /// <summary>
        /// This constructor Initializes the logger using the provided configuration and configuration section,
        /// call InitializeLogger method to initialize the logger with new configuration
        /// *** This Constructor is not used and cannot be used by the Dependency Injection Container *** 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpContextAccessor"></param>
        public SerilogHelper(IConfigurationRoot configuration, IHttpContextAccessor httpContextAccessor, string configurationSection)
            : base(httpContextAccessor)
        {
            _logger = CreateLogger(configuration, configurationSection);
        }
        #endregion

        #region Methods
        /// <summary>
        ///  This method re-initializeses the logger with the provided configuration every time it is called.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>Serilog.ILogger</returns>
        public ILogger InitializeLogger(IConfigurationRoot configuration, string configurationSection)
        {
            return _logger = CreateLogger(configuration, configurationSection);
        }

        /// <summary>
        /// This method initializeses the static logger with the provided configuration.
        /// The logger will be initialized one times regardless of how many times this method is called, unless 
        /// reInitialize flag is set to TRUE
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="reInitialize"></param>
        /// <returns>Serilog.ILogger</returns>
        public static ILogger InitializeStaticLogger(IConfigurationRoot configuration, bool reInitialize = false)
        {
            if (_staticLogger == null || reInitialize)
            {
                _staticLogger = CreateLogger(configuration);
            }

            return _staticLogger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customConfigurationFile">
        /// JSON Configuration file. If not specified appsettings.json is used as primary config file and 
        /// the current enverionment specific appsetting file as secondary (if present)
        /// </param>
        /// <returns></returns>
        public static IConfigurationRoot BuildConfiguration(string customConfigurationFile = null)
        {
            IConfigurationBuilder config = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory);

            if (!string.IsNullOrEmpty(customConfigurationFile))
            {
                config = config.AddJsonFile(customConfigurationFile, optional: false, reloadOnChange: false);
            }
            else
            {
                config = config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != null)
            {
                config = config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: false);
            }

            return config.AddEnvironmentVariables().Build();
        }
        #endregion

        #region Properities
        public override ILogger Logger => _logger;
        public static ILogger StaticLogger => _staticLogger;
        #endregion

        #region Private Methods
        private static ILogger CreateLogger(IConfigurationRoot configuration, string configurationSection = "Serilog")
        {
            //Initialize Logger using the provided configuration
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                       .WithDefaultDestructurers()
                       .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))
                .ReadFrom.Configuration(configuration, configurationSection)
                       .CreateLogger();
        }
        #endregion
    }
}
