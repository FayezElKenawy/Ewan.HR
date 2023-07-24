using Ewan.HR.Core.Application.Services.Attendance;
using MongoDB.Driver.Linq;
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
            await _attendanceService.GetEmployeesAttendance(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), null);
        }
    }
}
