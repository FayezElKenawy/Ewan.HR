using Microsoft.Extensions.DependencyInjection;
using SharedCoreLibrary.Domain.Abstractions;

namespace SharedCoreLibrary.Application.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddSharedCoreLibraryUnitOfWork<TUnitOfWork>(this IServiceCollection services)
            where TUnitOfWork : class, IHrUnitOfWork
        {
            services.AddScoped<IHrUnitOfWork, TUnitOfWork>();
        }

        public static void AddSharedCoreLibraryUnitOfWork<TUnitOfWork>(this IServiceCollection services,
            Func<IServiceProvider, TUnitOfWork> implementationFactory)
            where TUnitOfWork : class, IHrUnitOfWork
        {
            services.AddScoped<IHrUnitOfWork>(implementationFactory);
        }
    }
}
