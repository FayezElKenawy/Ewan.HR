using Microsoft.Extensions.DependencyInjection;
using SharedCoreLibrary.Application.Services;
using SharedInfraStructureLibrary.Interceptors;
using SharedInfraStructureLibrary.Services;

namespace SharedInfraStructureLibrary.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddInfraStructureServices(this IServiceCollection services)
        {
            services.AddScoped<IApiHelper, ApiHelper>();
            services.AddScoped<ICodeHelper, CodeHelper>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddSingleton<UpdateAuditDataInterceptor>();
        }

        public static void AddApiHelper(this IServiceCollection services)
        {
            services.AddScoped<IApiHelper, ApiHelper>();
        }

        public static void AddCodeHelper(this IServiceCollection services)
        {
            services.AddScoped<ICodeHelper, CodeHelper>();
        }


        public static void AddLocalizationService(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationService, LocalizationService>();
        }


        public static void AddUpdateAuditDataInterceptor(this IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditDataInterceptor>();
        }
    }
}
