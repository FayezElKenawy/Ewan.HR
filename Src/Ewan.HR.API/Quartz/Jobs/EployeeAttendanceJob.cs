using Ewan.HR.Core.Application.Services.Attendance;
using Quartz;

namespace Ewan.HR.API.Quartz.Jobs
{
    public class EmployeeAttendanceJob : IJob
    {
        private readonly IAttendanceService _attendanceService;
        public EmployeeAttendanceJob(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
             //await _attendanceService.AddEmployeesAttendance(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), null);
            await _attendanceService.AddEmployeesAttendance(null, null, null);

        }
    }
}
