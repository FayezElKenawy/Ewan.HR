using Ewan.Finance.Core.Application.Services;
using Ewan.HR.Core.Application.Services.Attendance;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAbsent;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcOvertime;
using Ewan.HR.Core.Application.Services.External.BioTimeData.GetData;
using Ewan.HR.Core.Application.Services.External.Identity;
using Ewan.HR.Core.Application.Services.PayRoll;
using Ewan.HR.Core.Application.Services.Request.Internal.InternalItemsServices;
using Ewan.HR.Core.Application.Services.Request.Internal.InternalRequestServices;
using Ewan.HR.Core.Application.Services.Request.MasterData;
using Ewan.HR.Core.Application.Services.Request.Vacation;
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

            #region Request
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IVacationService, VacationService>();
            services.AddScoped<IInternalRequestItemService, InternalRequestItemService>();
            services.AddScoped<IInternalRequestService, InternalRequestService>();
            #endregion

            #region Attendance
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<ICalcAttendanceService,CalcAttendanceService>();
            services.AddScoped<IGetOutsideDataService, GetOutsideDataService>();
            services.AddScoped<ICalcAbsentTime, CalcAbsentTime>();
            services.AddScoped<ICalcOvertime, CalcOvertime>();
            #endregion

            #region PayRoll
            services.AddScoped<IPayRollService,PayRollService>();
            #endregion

            #region TransactionLogger
            services.AddScoped<ITransactionLoggerService, TransactionLoggerService>();

            #endregion

        }
    }
}
