using Ewan.Finance.Core.Application.Services;
using Ewan.HR.Core.Application.Services.External.Identity;
using FluentValidation;
using MediatR;
using SharedCoreLibrary.Application.Common.Behaviours;
using System.Reflection;

namespace Ewan.HR.API.Common.Extensions
{
    public static class SeviceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            #region External
            services.AddScoped<IIdentityService, IdentityService>();
            #endregion

            #region TransactionLogger
            services.AddScoped<ITransactionLoggerService, TransactionLoggerService>();
            #endregion
        }
    }
}
